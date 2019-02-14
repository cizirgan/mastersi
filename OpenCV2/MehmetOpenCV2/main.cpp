
#include <opencv\cv.h>      
#include <opencv\ml.h>      
#include <stdio.h>
#include <opencv2/core/core.hpp>
#include <opencv2/highgui/highgui.hpp>
#include <opencv2\ml\ml.hpp>
#include <iostream>
#include <opencv2\imgproc.hpp>


using namespace cv;
using namespace std;

int left();
int right();
int circleDetect();
int createImage();

int main() {
	//left();
	//right();
	//circleDetect();

	createImage();

}

int left(void)
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
		return -1;
	}
	if (testResponse.read_csv(RL_TEST_RESPONSE) != 0)
	{
		fprintf(stderr, "Can't read csv file %s\n", RL_TEST_RESPONSE);
		return -1;
	}
	if (trainData.read_csv(RL_TRAIN) != 0)
	{
		fprintf(stderr, "Can't read csv file %s\n", RL_TRAIN);
		return -1;
	}
	if (trainResponse.read_csv(RL_TRAIN_RESPONSE) != 0)
	{
		fprintf(stderr, "Can't read csv file %s\n", RL_TRAIN_RESPONSE);
		return -1;
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

	waitKey(0);
	return 0;

}

int right(void)
{

	CvMLData testData;
	CvMLData testResponse;
	CvMLData trainData;
	CvMLData trainResponse;

	const char *TEST_FILE = "LR_TEST.csv";
	const char *TEST_RESPONSE_FILE = "LR_TEST_RESPONSE.csv";
	const char *TRAIN_FILE = "LR_TRAIN.csv";
	const char *TRAIN_RESPONSE_FILE = "LR_TRAIN_RESPONSE.csv";

	if (testData.read_csv(TEST_FILE) != 0)
	{
		fprintf(stderr, "Can't read csv file %s\n", TEST_FILE);
		return -1;
	}
	if (testResponse.read_csv(TEST_RESPONSE_FILE) != 0)
	{
		fprintf(stderr, "Can't read csv file %s\n", TEST_RESPONSE_FILE);
		return -1;
	}
	if (trainData.read_csv(TRAIN_FILE) != 0)
	{
		fprintf(stderr, "Can't read csv file %s\n", TRAIN_FILE);
		return -1;
	}
	if (trainResponse.read_csv(TRAIN_RESPONSE_FILE) != 0)
	{
		fprintf(stderr, "Can't read csv file %s\n", TRAIN_RESPONSE_FILE);
		return -1;
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

		printf("Response: %.3f", testResponseMatrix.at<float>(i, 0));
		printf(" Predicted: %.3f", predictions.at<float>(i, 0));
		printf("  < %.3f", testMatrix.at<float>(i, 8));
		printf("\n");

		/*
		cout << testResponseMatrix.at<float>(i, 0) << " "
		<< predictions.at<float>(i, 0) << " > "
		<< testMatrix.at<float>(i, 3) << endl;
		*/
	}

	cout << predictions.rows << endl;

	waitKey(0);
	return 0;

}

int circleDetect() {
	const char* filename = "abajo.jpg";
	// Loads an image
	Mat src = imread(filename, IMREAD_COLOR);
	// Check if image is loaded fine
	if (src.empty()) {
		printf(" Error opening image\n");
		printf(" Program Arguments: [image_name -- default %s] \n", filename);
		return -1;
	}
	Mat gray;
	cvtColor(src, gray, COLOR_BGR2GRAY);
	medianBlur(gray, gray, 5);
	vector<Vec3f> circles;

	HoughCircles(gray, circles, CV_HOUGH_GRADIENT, 1,
		gray.rows / 16,  // change this value to detect circles with different distances to each other
		100, 30, 1, 30 // change the last two parameters
					   // (min_radius & max_radius) to detect larger circles
	);
	for (size_t i = 0; i < circles.size(); i++)
	{
		Vec3i c = circles[i];
		Point center = Point(c[0], c[1]);
		// circle center
		circle(src, center, 1, Scalar(0, 100, 100), 3);
		// circle outline
		int radius = c[2];
		circle(src, center, radius, Scalar(255, 0, 255), 3);
	}
	imshow("detected circles", src);
	waitKey();
	return 0;
}

int createImage() {

	CvMLData testData;
	const char *RL_TEST = "RL_TEST.csv";

	if (testData.read_csv(RL_TEST) != 0)
	{
		fprintf(stderr, "Can't read csv file %s\n", RL_TEST);
		return -1;
	}

	// TODO > Read the coordinates from CSV file, create the circles and save the image.

	Mat grHistogram(800, 600, CV_8UC3, Scalar(255, 255, 255));
	Point center = Point(50, 50);
	Point center2 = Point(100, 50);
	line(grHistogram, center, center2, Scalar(0, 0, 0), 1, 8, 0);
	
	Point centerOfCircle = Point(400, 300);
	
	circle(grHistogram, centerOfCircle, 100, Scalar(0, 0, 0),5,8);

	namedWindow("Display window", WINDOW_AUTOSIZE);// Create a window for display.
	imshow("Display window", grHistogram);
	waitKey();
	return 0;
}