#pragma once
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

using namespace System;

namespace AlgorithmWrapper {
	public value struct WrapperPixels
	{
		int fposX; int fposY;
		int fR; int fG; int fB;

		int sposX; int sposY;
		int sR; int sG; int sB;

		int tposX; int tposY;
		int tR; int tG; int tB;
	};
	public ref class WrapperClass
	{
	public:
		WrapperClass();

		void loadImage(char* path);
		void saveImage(char* path);

		void generateKeys(double* key1, double* key2);
		void preparingRules(double key1, double key2);
	
		WrapperPixels scramblingPixels(int i, int j, int k);
		WrapperPixels descramblingPixels(int i, int j, int k);

	private:
		ScramblingImage* obj;
	};
}
