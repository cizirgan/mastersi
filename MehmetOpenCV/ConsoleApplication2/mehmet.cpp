#include "stdafx.h"
#include "mehmet.h"
#include <opencv2/opencv.hpp>

using namespace cv;
using namespace std;

int MehmetImage::convertToGrayScale(string imageFile)
{
	// Read image
	Mat image = imread(imageFile);

	// Check for invalid input
	if (image.empty())
	{
		cout << "Could not open or find the image" << endl;
		return EXIT_FAILURE;
	}

	// Convert color image to gray
	Mat grayImage;
	cvtColor(image, grayImage, COLOR_BGR2GRAY);

	// Save result
	imwrite("GrayScaleImage.jpg", grayImage);

	// Create a window for display.
	namedWindow("image", WINDOW_AUTOSIZE);
	namedWindow("gray image", WINDOW_NORMAL);

	// Display image
	imshow("image", image);
	imshow("gray image", grayImage);
	// Wait for a keystroke in the window
	waitKey(0);

	return EXIT_SUCCESS;
}

int MehmetImage::cropAndResize(string imageFile) {
	Mat source, scaleDown, scaleUp;

	// Read source image
	source = imread(imageFile, 1);

	// Scaling factors
	double scaleX = 0.6;
	double scaleY = 0.6;

	//The resize has the following prototype:
	// resize(sourceImage, destinationImage, Size(,), scale factor in x 
	// direction, scale factor in y direction, interpolation method) 
	// We can either specify the Size(,) of the output to determine the height
	// and width of the output or we can add the scaling factors and the Size 
	// will be calculated automatically. 

	// Scaling down the image 0.6 times
	cv::resize(source, scaleDown, Size(), scaleX, scaleY, INTER_LINEAR);
	// Scaling up the image 1.8 times
	cv::resize(source, scaleUp, Size(), scaleX * 3, scaleY * 3, INTER_LINEAR);

	//For cropping we use the Range function of OpenCV and then first specify 
	//the rows and then the columns of the cropping region. 

	//Cropped image
	Mat crop = source(cv::Range(50, 150), cv::Range(20, 200));

	// Create Display windows for all three images
	namedWindow("Original", WINDOW_AUTOSIZE);
	namedWindow("Scaled Down", WINDOW_AUTOSIZE);
	namedWindow("Scaled Up", WINDOW_AUTOSIZE);
	namedWindow("Cropped Image", WINDOW_AUTOSIZE);

	// Show images
	imshow("Original", source);
	imshow("Scaled Down", scaleDown);
	imshow("Scaled Up", scaleUp);
	imshow("Cropped Image", crop);

	waitKey(0);
	return  EXIT_SUCCESS;
}