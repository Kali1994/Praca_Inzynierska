#pragma once
#include "C:\Users\Kali\Desktop\Praca_Inzynierska\Application\Application\ScramblingImage.h"
#include "C:\Users\Kali\Desktop\Praca_Inzynierska\Application\Application\ScramblingImage.cpp"
#include "C:\Users\Kali\Desktop\Praca_Inzynierska\Application\Application\Picture.h"
#include "C:\Users\Kali\Desktop\Praca_Inzynierska\Application\Application\Picture.cpp"

using namespace System;

namespace AlgorithmWrapper {
	public ref class WrapperClass
	{
	public:
		WrapperClass();

		void setTestValue(int);
		int getTestValue();

		void loadImage(char* path);
		void saveImage(char* path);
		// TODO: Add your methods for this class here.
	private:
		ScramblingImage* obj;
	};
}
