
#include <opencv\cv.h>      
#include <opencv\ml.h>      
#include <stdio.h>
#include <opencv2/core/core.hpp>
#include <opencv2/highgui/highgui.hpp>
#include <opencv2\ml\ml.hpp>
#include <iostream>

using namespace cv;
using namespace std;


void lefttoright()
{

	CvMLData testData;
	CvMLData testResponse;
	CvMLData trainData;
	CvMLData trainResponse;

	const char *RL_TEST = "RL_TEST.csv";
	const char *RL_TEST_RESPONSE = "RL_TEST_RESPONSE.csv";
	const char *RL_TRAIN = "RL_TRAIN.csv";
	const char *RL_TRAIN_RESPONSE = "RL_TRAIN_RESPONSE.csv";

	if (testData.read_csv(RL_TEST) != 0)
	{
		fprintf(stderr, "Can't read csv file %s\n", RL_TEST);
	
	}
	if (testResponse.read_csv(RL_TEST_RESPONSE) != 0)
	{
		fprintf(stderr, "Can't read csv file %s\n", RL_TEST_RESPONSE);
	
	}
	if (trainData.read_csv(RL_TRAIN) != 0)
	{
		fprintf(stderr, "Can't read csv file %s\n", RL_TRAIN);
		
	}
	if (trainResponse.read_csv(RL_TRAIN_RESPONSE) != 0)
	{
		fprintf(stderr, "Can't read csv file %s\n", RL_TRAIN_RESPONSE);
		
	}

	Mat testMatrix = testData.get_values();
	Mat testResponseMatrix = testResponse.get_values();

	Mat trainMatrix = trainData.get_values();
	Mat trainResponseMatrix = trainResponse.get_values();

	cout << "Test Matrix: " << testMatrix.rows << " " << testMatrix.cols << endl;
	cout << "Test Response Matrix: " << testResponseMatrix.rows << " " << testResponseMatrix.cols << endl;
	cout << "Train Matrix: " << trainMatrix.rows << " " << trainMatrix.cols << endl;
	cout << "Train Response Matrix: " << trainResponseMatrix.rows << " " << trainResponseMatrix.cols << endl;



	CvANN_MLP mlp;
	CvANN_MLP_TrainParams params;
	CvTermCriteria criteria;

	Mat layers = Mat(3, 1, CV_32SC1);
	layers.row(0) = Scalar(9);
	layers.row(1) = Scalar(5);
	layers.row(2) = Scalar(1);

	criteria.max_iter = 100;
	criteria.epsilon = 0.01;
	criteria.type = CV_TERMCRIT_ITER | CV_TERMCRIT_EPS;
	params.train_method = CvANN_MLP_TrainParams::BACKPROP;
	params.bp_dw_scale = 0.05;
	params.bp_moment_scale = 0.05;
	params.term_crit = criteria;
	mlp.create(layers);

	mlp.train(trainMatrix, trainResponseMatrix, Mat(), Mat(), params);

	Mat predictions;

	mlp.predict(testMatrix, predictions);



	for (size_t i = 0; i < predictions.rows; i++)
	{
		//cout << "Value: " << predictions.at<float>(i, 0) << " Real Value " <<testResponseMatrix.at<float>(i,0) << endl;

		printf("%.3f", testResponseMatrix.at<float>(i, 0));
		printf(" %.3f", predictions.at<float>(i, 0));
		printf("  > %.3f", testMatrix.at<float>(i, 8));
		printf("\n");

		/*
		cout << testResponseMatrix.at<float>(i, 0) << " "
		<< predictions.at<float>(i, 0) << " > "
		<< testMatrix.at<float>(i, 3) << endl;
		*/
	}

	cout << predictions.rows << endl;




}