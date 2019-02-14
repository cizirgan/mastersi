#include <opencv2/opencv.hpp> 
using namespace cv;
using namespace std;

// This program reads an image using imread() from the images directory, 
// converts it to gray scale using cvtColor() and displays it using imshow(). 
// It also saves the image to the disk using the imwrite() function.
int mehmet(void)
{

	// Read image
	Mat image = imread("MehmetImage.jpg");

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
	imwrite("MehmetImage.jpg", grayImage);

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