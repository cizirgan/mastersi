
#include "stdafx.h"
#include <vector>
#include <stdio.h>
#include <opencv2/opencv.hpp>
#include "mehmet.h"  

using namespace cv;
using namespace std;


int main(int argv, char **argc)
{
	MehmetImage a;
	//a.convertToGrayScale("MehmetImage.jpg");
	a.cropAndResize("MehmetImage.jpg");

	waitKey(0);
	return 0;
}

