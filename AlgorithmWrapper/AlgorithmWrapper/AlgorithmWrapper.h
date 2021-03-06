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
using namespace System::Threading;

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
	public value struct WrapperPixel
	{
		int posX;
		int posY;
		int R;
		int G;
		int B;
	};
	public ref class ThreadParams
	{
	public:
		Thread^ myFirst;
		Thread^ mySecond;
		Thread^ myThird;
		Thread^ myFourth;
		ThreadParams(Thread^ first, Thread^ second, Thread^ third, Thread^ fourth)
		{
			myFirst = first;
			mySecond = second;
			myThird = third;
			myFourth = fourth;
		}
	};

	public ref class ThreadParamsPos
	{
	public:
		int x;
		int y;
		ThreadParamsPos(int i, int j)
		{
			x = i;
			y = j;
		}
	};

	public ref class ThreadX
	{
	public:
		ThreadX(ScramblingImage*);

		void calculateRulesKMR();
		void calculateRulesRNS();

		void knightTravel(Object^ paramObj);
		void waitForThreads(Object^ paramObj);

		static int* m_chessBoard = new int[64];
		static bool m_knightTourFinded = false;
		static Object^ Lock = gcnew Object;
	private:
		ScramblingImage* m_obj;
	};
	public ref class WrapperClass
	{
	public:
		WrapperClass();

		bool loadImage(char* path);
		void saveImage(char* path);

		void generateKeys(double* key1, double* key2);
		void preparingRules(double key1, double key2);
		void threadPreparingRules(double key1, double key2);
	
		WrapperPixels scramblingPixels(int i, int j, int k);
		WrapperPixels descramblingPixels(int i, int j, int k);

		WrapperPixels threadScramblingPixels(int i, int j, int k, int startX, int endX, int startY, int endY);
		WrapperPixels threadDescramblingPixels(int i, int j, int k, int startX, int endX, int startY, int endY);

		WrapperPixel getPixel(int posX, int posY);

	private:
		ScramblingImage* obj;
	};
}
