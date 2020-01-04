// stdafx.h : include file for standard system include files,
// or project specific include files that are used frequently, but
// are changed infrequently
//

#pragma once

#define M_PI 3.14159265358979323846
#include "targetver.h"

#include <string>
#include <iostream>
#include <cstdint>
#include <cmath>
#include <opencv2/opencv.hpp>

using namespace std;
using namespace cv;

struct Pixel
{
	int posX;
	int posY;

	int R;
	int G;
	int B;

	void setValue(uint8_t* pixelValue)
	{
		R = pixelValue[0];
		G = pixelValue[1];
		B = pixelValue[2];
	}
};

uint8_t*** allocateMemory(uint8_t***, int, int);
void deallocateMemory(uint8_t***, int, int);
// TODO: reference additional headers your program requires here
