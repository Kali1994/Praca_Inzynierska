#pragma once
#include "stdafx.h"

#include "AlgorithmWrapper.h"
#include "C:\Users\Kali\Desktop\Praca_Inzynierska\Application\Application\ScramblingImage.h"
#include "C:\Users\Kali\Desktop\Praca_Inzynierska\Application\Application\ScramblingImage.cpp"
#include "C:\Users\Kali\Desktop\Praca_Inzynierska\Application\Application\Picture.h"
#include "C:\Users\Kali\Desktop\Praca_Inzynierska\Application\Application\Picture.cpp"
#include "C:\Users\Kali\Desktop\Praca_Inzynierska\Application\Application\KnightTourGenerator.h"
#include "C:\Users\Kali\Desktop\Praca_Inzynierska\Application\Application\KnightTourGenerator.cpp"
#include "C:\Users\Kali\Desktop\Praca_Inzynierska\Application\Application\PRBG.h"
#include "C:\Users\Kali\Desktop\Praca_Inzynierska\Application\Application\PRBG.cpp"
#include "C:\Users\Kali\Desktop\Praca_Inzynierska\Application\Application\BitOperation.h"
#include "C:\Users\Kali\Desktop\Praca_Inzynierska\Application\Application\BitOperation.cpp"
#include "C:\Users\Kali\Desktop\Praca_Inzynierska\Application\Application\Encryption.h"
#include "C:\Users\Kali\Desktop\Praca_Inzynierska\Application\Application\Encryption.cpp"
#include "C:\Users\Kali\Desktop\Praca_Inzynierska\Application\Application\Extract.h"
#include "C:\Users\Kali\Desktop\Praca_Inzynierska\Application\Application\Extract.cpp"
#include "C:\Users\Kali\Desktop\Praca_Inzynierska\Application\Application\stdafx.h"
#include "C:\Users\Kali\Desktop\Praca_Inzynierska\Application\Application\stdafx.cpp"

AlgorithmWrapper::WrapperClass::WrapperClass()
{
	obj = new ScramblingImage();
}

void AlgorithmWrapper::WrapperClass::loadImage(char* path)
{
	obj->loadImage(path);
}

void AlgorithmWrapper::WrapperClass::saveImage(char* path)
{
	obj->saveImage(path);
}

void AlgorithmWrapper::WrapperClass::generateKeys(double* key1, double* key2)
{
	obj->generateKeys(*key1, *key2);
}

void AlgorithmWrapper::WrapperClass::preparingRules(double key1, double key2)
{
	obj->preparingRules(key1, key2);
}

AlgorithmWrapper::WrapperPixels AlgorithmWrapper::WrapperClass::scramblingPixels(int i, int j, int k)
{
	Pixel* tempPixels = obj->scramblingPixels(i, j, k);

	WrapperPixels pixels;

	pixels.fposX = tempPixels[0].posX;
	pixels.fposY = tempPixels[0].posY;
	pixels.fR = tempPixels[0].R;
	pixels.fG = tempPixels[0].G;
	pixels.fB = tempPixels[0].B;

	pixels.sposX = tempPixels[1].posX;
	pixels.sposY = tempPixels[1].posY;
	pixels.sR = tempPixels[1].R;
	pixels.sG = tempPixels[1].G;
	pixels.sB = tempPixels[1].B;

	pixels.tposX = tempPixels[2].posX;
	pixels.tposY = tempPixels[2].posY;
	pixels.tR = tempPixels[2].R;
	pixels.tG = tempPixels[2].G;
	pixels.tB = tempPixels[2].B;
	
	return pixels;
}

AlgorithmWrapper::WrapperPixels AlgorithmWrapper::WrapperClass::descramblingPixels(int i, int j, int k)
{
	Pixel* tempPixels = obj->descramblingPixels(i, j, k);

	WrapperPixels pixels;

	pixels.fposX = tempPixels[0].posX;
	pixels.fposY = tempPixels[0].posY;
	pixels.fR = tempPixels[0].R;
	pixels.fG = tempPixels[0].G;
	pixels.fB = tempPixels[0].B;

	pixels.sposX = tempPixels[1].posX;
	pixels.sposY = tempPixels[1].posY;
	pixels.sR = tempPixels[1].R;
	pixels.sG = tempPixels[1].G;
	pixels.sB = tempPixels[1].B;

	pixels.tposX = tempPixels[2].posX;
	pixels.tposY = tempPixels[2].posY;
	pixels.tR = tempPixels[2].R;
	pixels.tG = tempPixels[2].G;
	pixels.tB = tempPixels[2].B;

	return pixels;
}

