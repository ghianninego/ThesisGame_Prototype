%%%%%%%%%%% DATABASE PREPROCESSING AND TRAINING %%%%%%%%%%%%%%%%%%%%%

%%%%%%%%%%%%%%%%%%%%% READ DATABASE %%%%%%%%%%%%%%%%%%%%%%%%%%%
fid = fopen('faces.csv');
tline = fgetl(fid);
array = char.empty;
A = double.empty; % columns are pixels, rows are images
labels = [];
while ischar(tline)
    %array =[array;tline];
    
    tempPath = strsplit(tline,';');
    label = char(tempPath(2));
    labels = [labels;label];
    image = imread(char(tempPath(1)));
    if(size(size(image),2) == 3)
        image = image(:,:,1);
    end
    image = imresize(image,[200,200]);
    
    image = double(image);
    B = reshape(image',[size(image,1)*size(image,2),1]);
    A = [A,B];
    
    tline = fgetl(fid);
end
fclose(fid);
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

%%%%%%%%%%%%%%%%% COMPUTE PCA %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
A = A';
trainMean = mean(A); 
[trainPCA,testScore] = pca(A); 
numDim = 50; % # of components
reducedTrainData = bsxfun(@minus, A, trainMean) * trainPCA(:,1:numDim); %feature space
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

%%%%%%%%%%%%%%%%%%SVM TRAINING%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
rng(1);
tempSVM = templateSVM('KernelFunction','rbf','KernelScale','auto','BoxConstraint',5);
modelSVM = fitcecoc(reducedTrainData,labels,'Learners',tempSVM);
%CVSVMModel = crossval(modelSVM);
%L = kfoldLoss(CVSVMModel)
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

%%%%%%%%%%%%%%%%%%%%%% PREPARATION %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
faceDetector = vision.CascadeObjectDetector(); %Viola Jones Face Detector
pointTracker = vision.PointTracker('MaxBidirectionalError', 2); %KLT Tracker

vid = imaq.VideoDevice('winvideo',1,'MJPG_640x480');

videoFrame = step(vid);
videoFrame = fliplr(videoFrame);
videoPlayer = vision.VideoPlayer('Position', [100 100 [size(videoFrame,2), size(videoFrame,1)]+30]);
videoPlayer2 = vision.VideoPlayer();
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

c = 0;
croppedImage = [];
prevEmotion = 999;
%%%%%%%%%%%%%%%%%%%%%%% LIVE VIDEO %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
% Track the face over successive video frames until the video is finished.
runLoop = true;
numPts = 0; %number of points to track
while runLoop

    % Extract the next video frame
    videoFrame = step(vid);
    videoFrameGray = rgb2gray(videoFrame);
    
  
  if numPts < 10
        %%%%%%%%%%%%%%%%%%%% DETECTION %%%%%%%%%%%%%%%%%%%%%%%%%%
        bbox = faceDetector.step(videoFrameGray);
     
        %%%%%%%%%%%%%% GETTING THE LARGEST FACE %%%%%%%%%%%%%%%%%
        maxFace = 0;
        tempRect = [];
        for i=1:size(bbox,1)
            if(maxFace < (bbox(i,3)*bbox(i,4)))
                maxFace = bbox(i,3)*bbox(i,4);
                tempRect = bbox(i,:);
            end
        end
        bbox = tempRect;
        %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        
        if ~isempty(bbox)
            %%%%%%%%%%%%%%%REDUCING BOUNDING BOX SIZE%%%%%%%%%%%%%%%%
            xPos = bbox(1);
            yPos = bbox(2);
            width = bbox(3);
            height = bbox(4);

            percentReduction = 0.23;

            newWidth = width - (width * percentReduction);
            newHeight = height - (height * percentReduction);

            bbox(3) = newWidth;
            bbox(4) = newHeight;

            deltaX = (width - newWidth)/2;
            deltaY = (height - (newHeight))/2;

            bbox(1) = xPos +deltaX;
            bbox(2) = yPos + deltaY+deltaY/4;
            %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
            
            croppedImage = imcrop(videoFrameGray,bbox);
            faceRect = bbox;
            %%%%%%%%%%%%%%%%%% SETTING UP POINTS %%%%%%%%%%%%%%%%%%%%
            % Find corner points inside the detected region.
            points = detectMinEigenFeatures(videoFrameGray, 'ROI', bbox(1, :));
            % Re-initialize the point tracker.
            xyPoints = points.Location;
            numPts = size(xyPoints,1);
            release(pointTracker);
            initialize(pointTracker, xyPoints, videoFrameGray);
            % Save a copy of the points.
            oldPoints = xyPoints;
            % Convert the rectangle represented as [x, y, w, h] into an
            % M-by-2 matrix of [x,y] coordinates of the four corners. This
            % is needed to be able to transform the bounding box to display
            % the orientation of the face.
            bboxPoints = bbox2points(bbox(1, :));
            % Convert the box corners into the [x1 y1 x2 y2 x3 y3 x4 y4]
            % format required by insertShape.
            bboxPolygon = reshape(bboxPoints', 1, []);
            %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
            
            %%%%%%%%%%%%%%%%% SETTING UP FRAME %%%%%%%%%%%%%%%%%%%%%%
            % Display a bounding box around the detected face.
            videoFrame = insertShape(videoFrame, 'Polygon', bboxPolygon, 'LineWidth', 3);
            % Display detected corners.
            videoFrame = insertMarker(videoFrame, xyPoints, 'o', 'Color', 'white');
            %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        end
        %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
  else
        %%%%%%%%%%%%%%%%%%%%%%% KLT %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        [xyPoints, isFound] = step(pointTracker, videoFrameGray);
        visiblePoints = xyPoints(isFound, :);
        oldInliers = oldPoints(isFound, :);

        numPts = size(visiblePoints, 1);

        if numPts >= 10
            %%%%%%%%%%%%%%% FIND TRACKED POINTS %%%%%%%%%%%%%%%%%%%%%
            % Estimate the geometric transformation between the old points
            % and the new points.
            [xform, oldInliers, visiblePoints] = estimateGeometricTransform(...
                oldInliers, visiblePoints, 'similarity', 'MaxDistance', 4);
            % Apply the transformation to the bounding box.
            bboxPoints = transformPointsForward(xform, bboxPoints);
            % Convert the box corners into the [x1 y1 x2 y2 x3 y3 x4 y4]
            % format required by insertShape.
            bboxPolygon = reshape(bboxPoints', 1, []);
            %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
            
            %%%%%%%%%%%%%%%%% UPDATE CROPPED IMAGE %%%%%%%%%%%%%%%%%%
            sizeOfBox = min(abs(bboxPolygon(1)-bboxPolygon(3)), abs(bboxPolygon(4)-bboxPolygon(6)));
            bbox = ([bboxPolygon(1),bboxPolygon(2),sizeOfBox,sizeOfBox]);
            croppedImage = imcrop(videoFrameGray, bbox);
            faceRect = bbox;
            %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
            
            %%%%%%%%%%%%%%%%% UPDATE VIDEO FRAME %%%%%%%%%%%%%%%%%%%%
            % Display a bounding box around the face being tracked.
            videoFrame = insertShape(videoFrame, 'Polygon', bboxPolygon, 'LineWidth', 3);
            % Display tracked points.
            videoFrame = insertMarker(videoFrame, visiblePoints, '+', 'Color', 'white');
            %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
            
            %%%%%%%%%%%%%%%%%%% RESET POINTS%%%%%%%%%%%%%%%%%%%%%%%%%
            oldPoints = visiblePoints;
            setPoints(pointTracker, oldPoints);
            %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        end
        %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
   end
  %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
  if ~isempty(bbox)
  %%%%%%%%%%%%%%%%%%%%%% EQUALIZATION %%%%%%%%%%%%%%%%%%%%%%%%%%%%%
         croppedImage = imresize(croppedImage,[200,200]);
         croppedImage = uint8(croppedImage*255);
         grayImage3D(:,:,1) = croppedImage;
         grayImage3D(:,:,2) = croppedImage;
         grayImage3D(:,:,3) = croppedImage;
         processedImage = rgb2hsv(grayImage3D);
         vChannel = processedImage(:,:,3);
         vChannel = histeq(vChannel);
         processedImage(:,:,3) = vChannel;
         croppedImage = rgb2gray(hsv2rgb(processedImage));
      %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%% 

      %%%%%%%%%%%%%%%%%%%%%%%%% TESTING %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
         croppedImage2 = double(uint8(croppedImage*255));
        [irow, icol] = size(croppedImage2);
        testData = reshape(croppedImage2',1,irow*icol);
        reducedTestData = bsxfun(@minus, testData, trainMean) * trainPCA(:, 1:numDim);
        [testLabel,x,y] = predict(modelSVM,reducedTestData);

            videoFrame = insertObjectAnnotation(videoFrame,'rectangle',bbox,testLabel,'Color','blue');

      %end
      %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

      %%%%%%%%%%%%%%%%%%% DETERMINE EMOTION %%%%%%%%%%%%%%%%%%%%%%%%%%
      emotion = 'neutral';
      if testLabel == '0'
          emotion = 'neutral';
      elseif testLabel == '1'
          emotion = 'angry';
      elseif testLabel == '2'
          emotion = 'disgust';
      elseif testLabel == '3'
          emotion = 'fear';
      elseif testLabel == '4'
          emotion = 'happy';
      elseif testLabel == '5'
          emotion = 'sad';
      elseif testLabel == '6'
          emotion = 'surprise';
      end
      %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
      
      %%%%%%%%%%%%%%%%%%% UPDATE TEXT FILE %%%%%%%%%%%%%%%%%%%%%%%%%%%
        fileData = fopen('data.txt','w');
        if(fileData ~= -1)
            fprintf(fileData,'%s',emotion);
            fclose(fileData);
        end
        
      %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
 end
  %%%%%%%%%%%%%%%%%%%%% DISPLAY FRAMES %%%%%%%%%%%%%%%%%%%%%%%%%%%%
    step(videoPlayer2, croppedImage);
    % Display the annotated video frame using the video player object.
    videoFrame = fliplr(videoFrame);
    videoFrame = insertText(videoFrame,[0,0],emotion,'FontSize',18,'BoxColor','red','BoxOpacity',1,'TextColor','white');
    step(videoPlayer, videoFrame);
    % Check whether the video player window has been closed.
    runLoop = isOpen(videoPlayer);
  %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
end
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%


%%%%%%%%%%%%%%%%%%%%%%%%% CLEAN UP %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
release(vid);
release(videoPlayer);
release(videoPlayer2);
release(pointTracker);
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
