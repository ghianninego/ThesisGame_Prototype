%%%%%%%%%%%%%%%%%%%%% READ DATABASE %%%%%%%%%%%%%%%%%%%%%%%%%%%
fid = fopen('neutrals.csv');
tline = fgetl(fid);
array = char.empty;
A = double.empty; % columns are pixels, rows are images
labels = []; %row vector of labels
labels0 = [];
while ischar(tline)
   % array =[array;tline];
    tempPath = strsplit(tline,';');
    label = char(tempPath(2));
    if(label == '0')
        labels0 = [labels0;1];
    else
        labels0 = [labels0;0];
    end
    
    
    labels = [labels;label];
    image = imread(char(tempPath(1)));
    if(size(size(image),2) ~= 3)
        image2d = image;
    end
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
numDim = 5; % # of components
reducedTrainData = bsxfun(@minus, A, trainMean) * trainPCA(:,1:numDim); %feature space

%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

% %%%%%%%%%%%%%%%SAVING GENERATED PCA TABLE%%%%%%%%%%%%%%%%%%%%%
% PCAValues = fopen('PCAvalues.txt','wt');
% PCAValuesCOEFF = fopen('PCAvaluesCOEFF.txt','w+');
% fprintf(PCAValues,'%g, ',trainMean);
% fprintf(PCAValues,'\n\n');
% 
% for ii = 1:size(reducedTrainData,1)
%     fprintf(PCAValues,'%g, ',reducedTrainData(ii,:));
%     fprintf(PCAValues,'\n');
% end
% %fprintf(PCAValues,'\n\n');
% for ii = 1:size(trainPCA,1)
%     fprintf(PCAValuesCOEFF,'%g, ',trainPCA(ii,:));
%     fprintf(PCAValuesCOEFF,'\n');
% end
% 
% fclose(PCAValues);
% fclose(PCAValuesCOEFF);
% %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

%%%%%%%%%%%%%%%SAVING GENERATED PCA TABLE%%%%%%%%%%%%%%%%%%%%%
PCAValues = fopen('PCAvalues.csv','wt');
fprintf(PCAValues,'%s, ','Emotion');
for i = 1:size(reducedTrainData,2)
    fprintf(PCAValues,'Eigenface %d, ',i);
end
fprintf(PCAValues,'\n');
count = 1;
for ii = 1:size(reducedTrainData,1)
    fprintf(PCAValues,'%d, ', labels(count));
    fprintf(PCAValues,'%g, ',reducedTrainData(ii,:));
    fprintf(PCAValues,'\n');
    count = count + 1;
end

fclose(PCAValues);
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

%%%%%%%%%%%%%%%%%%SVM TRAINING%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
tempSVM = templateSVM('KernelFunction','linear','KernelScale','auto','BoxConstraint',5, 'Standardize',false);
model = fitcecoc(reducedTrainData,labels,'Learners',tempSVM);
modelSVM0 = fitcsvm(reducedTrainData,labels,'KernelFunction','rbf','KernelScale','auto','Standardize',false);
CVSVMModel = crossval(model);
L = kfoldLoss(CVSVMModel)
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

%%%%%%%%%%%%%%%% TEMP TO CHECK FOR ACCURACY %%%%%%%%%%%%%%%%%%
correct = 0;
incorrect = 0;
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

%%%%%%%%%%%%%%%%%%%TESTING%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
fid = fopen('faces.csv');
tline = fgetl(fid);
array = char.empty;
ansLabels = [];
while (ischar(tline))
    array = [array;tline];
    tempPath = strsplit(tline,';');
    image = double(imread(char(tempPath(1)))); %testImage
   % image = double(imread('image0.png'));
    image = image(:,:,1);
    image = imresize(image,[200,200]);
    
    [irow, icol] = size(image);
    testData = reshape(image',1,irow*icol);
    reducedTestData = bsxfun(@minus, testData, trainMean) * trainPCA(:, 1:numDim);
    testLabel = predict(modelSVM0, reducedTestData);
    actualLabel = char(tempPath(2));
    
    if actualLabel == '0'
        actualLabel = '1';
    else
        actualLabel = '0';
    end   
   
    if testLabel == actualLabel
        correct = correct + 1;
    else
        incorrect = incorrect + 1;
    end
     ansLabels = [ansLabels;testLabel];
    tline = fgetl(fid);
end
accuracy = (correct/size(array,1))*100
fclose(fid);
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%