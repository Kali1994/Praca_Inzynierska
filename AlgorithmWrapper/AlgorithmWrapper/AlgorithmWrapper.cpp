#pragma once
#include "stdafx.h"

#include "AlgorithmWrapper.h"
#include "C:\Users\Kali\Desktop\Praca_Inzynierska\Application\Application\ScramblingImage.h"
#include "C:\Users\Kali\Desktop\Praca_Inzynierska\Application\Application\ScramblingImage.cpp"
#include "C:\Users\Kali\Desktop\Praca_Inzynierska\Application\Application\Picture.h"
#include "C:\Users\Kali\Desktop\Praca_Inzynierska\Application\Application\Picture.cpp"

AlgorithmWrapper::WrapperClass::WrapperClass()
{
	obj = new ScramblingImage("s", "a");
}

void AlgorithmWrapper::WrapperClass::setTestValue(int value)
{
	obj->setTest(value);
}

int AlgorithmWrapper::WrapperClass::getTestValue()
{
	return obj->getTest();
}

void AlgorithmWrapper::WrapperClass::loadImage(char* path)
{
	obj->loadImage(path);
}

void AlgorithmWrapper::WrapperClass::saveImage(char* path)
{
	obj->saveImage(path);
}

