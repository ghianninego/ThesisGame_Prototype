faceDetector = vision.CascadeObjectDetector(); %Viola Jones Face Detector
pointTracker = vision.PointTracker('MaxBidirectionalError', 2); %KLT Tracker

vid = imaq.VideoDevice('winvideo',1,'MJPG_640x480');

videoFrame = step(vid);
videoFrame = fliplr(videoFrame);
videoPlayer = vision.VideoPlayer('Position', [100 100 [size(videoFrame,2), size(videoFrame,1)]+30]);
videoPlayer2 = vision.VideoPlayer();

% Track the face over successive video frames until the video is finished.
runLoop = true;
numPts = 0; %number of points to track
while runLoop

    % Extract the next video frame
    videoFrame = step(vid);
    videoFrameGray = rgb2gray(videoFrame);
    faceRect = [];
  %%%%%%%%%%%%%%%% KLT %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
  if numPts < 10
        % Detection mode.
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

            percentReduction = 0.1;

            newWidth = width - (width * percentReduction);
            newHeight = height - (height * percentReduction);

            bbox(3) = newWidth;
            bbox(4) = newHeight;

            deltaX = (width - newWidth)/2;
            deltaY = (height - (newHeight))/2;

            bbox(1) = xPos + deltaX;
            bbox(2) = yPos + deltaY + deltaY;
            %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
            
            croppedImage = imcrop(videoFrameGray,bbox);
            faceRect = bbox;
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
            
            % Display a bounding box around the detected face.
            videoFrame = insertShape(videoFrame, 'Polygon', bboxPolygon, 'LineWidth', 3);

            % Display detected corners.
            videoFrame = insertMarker(videoFrame, xyPoints, 'o', 'Color', 'white');
        end

    else
        % Tracking mode.
        [xyPoints, isFound] = step(pointTracker, videoFrameGray);
        visiblePoints = xyPoints(isFound, :);
        oldInliers = oldPoints(isFound, :);

        numPts = size(visiblePoints, 1);

        if numPts >= 10
            % Estimate the geometric transformation between the old points
            % and the new points.
            [xform, oldInliers, visiblePoints] = estimateGeometricTransform(...
                oldInliers, visiblePoints, 'similarity', 'MaxDistance', 4);

            % Apply the transformation to the bounding box.
            bboxPoints = transformPointsForward(xform, bboxPoints);

            % Convert the box corners into the [x1 y1 x2 y2 x3 y3 x4 y4]
            % format required by insertShape.
            bboxPolygon = reshape(bboxPoints', 1, []);
            
            sizeOfBox = min(abs(bboxPolygon(1)-bboxPolygon(3)), abs(bboxPolygon(4)-bboxPolygon(6)));
            tempBbox = ([bboxPolygon(1),bboxPolygon(2),sizeOfBox,sizeOfBox]);
            
            croppedImage = imcrop(videoFrameGray, tempBbox);
            faceRect = tempBbox;
            % Display a bounding box around the face being tracked.
            videoFrame = insertShape(videoFrame, 'Polygon', bboxPolygon, 'LineWidth', 3);

            % Display tracked points.
            %videoFrame = insertMarker(videoFrame, visiblePoints, '+', 'Color', 'white');

            % Reset the points.
            oldPoints = visiblePoints;
            setPoints(pointTracker, oldPoints);
        end

   end
  %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
  
  %%%%%%%%%%%%%%%%%%%%%% EQUALIZATION %%%%%%%%%%%%%%%%%%%%%%%%%%%%%
    croppedImage = imresize(croppedImage,[200,200]);
    grayImage3D(:,:,1) = croppedImage;
    grayImage3D(:,:,2) = croppedImage;
    grayImage3D(:,:,3) = croppedImage;
    processedImage = rgb2hsv(grayImage3D);
    vChannel = processedImage(:,:,3);
    vChannel = histeq(vChannel);
    processedImage(:,:,3) = vChannel;
    croppedImage = rgb2gray(hsv2rgb(processedImage));
  %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%% 
    %faceRect = [xPos, yPos, width, height]
    if ~isempty(faceRect)
        upperHalfRect = [faceRect(1),faceRect(2),faceRect(3),faceRect(4)/2];
        lowerHalfRect = [faceRect(1),faceRect(2)+faceRect(4)/2+faceRect(4)/8,faceRect(3),faceRect(4)/2];
        upperHalf = imcrop(videoFrame,upperHalfRect);
        lowerHalf = imcrop(videoFrame,lowerHalfRect);

        MouthDetect = vision.CascadeObjectDetector('Mouth','MergeThreshold',16);
        BBMouth=step(MouthDetect,lowerHalf);

        %To detect Eyes
        EyeDetect = vision.CascadeObjectDetector('EyePairBig');
        BBEyes=step(EyeDetect,upperHalf);
        %rectangle('Position',BB,'LineWidth',4,'LineStyle','-','EdgeColor','b');
        if ~isempty(BBMouth) && ~isempty(BBEyes)
            BBMouth = BBMouth(1,:);
            maxMouthSize = max([BBMouth(3)+ (BBMouth(3)*0.3),BBMouth(4)]);
            BBMouth(2) = BBMouth(2) + faceRect(2)+faceRect(4)/2+faceRect(4)/8;
            BBMouth(1) = BBMouth(1) + faceRect(1)-((BBMouth(3)*0.3)/2);
            BBMouth(3) = maxMouthSize;
            BBMouth(4) = maxMouthSize/2;
            % 1:2 ratio
            BBEyes = BBEyes(1,:);
            maxEyesSize = max([BBEyes(3),BBEyes(4)]);
            BBEyes(2) = BBEyes(2) + faceRect(2) - maxEyesSize/4;
            BBEyes(1) = BBEyes(1) + faceRect(1);
            %1:2 ratio
            BBEyes(3) = maxEyesSize;
            BBEyes(4) = maxEyesSize/2;
            
            videoFrame = insertShape(videoFrame, 'Rectangle', lowerHalfRect, 'LineWidth', 3, 'Color','red');
            videoFrame = insertShape(videoFrame, 'Rectangle', BBMouth, 'LineWidth', 3, 'Color','red');
            videoFrame = insertShape(videoFrame, 'Rectangle', upperHalfRect, 'LineWidth', 3, 'Color','blue');
            videoFrame = insertShape(videoFrame, 'Rectangle', BBEyes, 'LineWidth', 3, 'Color','red');
        end
    end
    step(videoPlayer2, croppedImage);
    % Display the annotated video frame using the video player object.
    videoFrame = fliplr(videoFrame);
    step(videoPlayer, videoFrame);
    % Check whether the video player window has been closed.
    runLoop = isOpen(videoPlayer);
  
    
end

% Release resources
release(vid);
release(videoPlayer);
release(videoPlayer2);
release(pointTracker);