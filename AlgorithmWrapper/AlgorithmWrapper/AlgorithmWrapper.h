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
	public ref class ThreadParams
	{
	public:
		Thread^ myFirst;
		Thread^ mySecond;
		Thread^ myThird;
		ThreadParams(Thread^ first, Thread^ second, Thread^ third)
		{
			myFirst = first;
			mySecond = second;
			myThird = third;
		}
	};
	public ref class ThreadX
	{
	public:
		ThreadX(ScramblingImage*);
		void calculateFirstChaoticMap(Object^ data);
		void calculateSecondChaoticMap(Object^ data);

		void calculateRulesKMR();
		void calculateRulesRNS();

		double* getFirstMap();
		double* getSecondMap();

		void knightTravel();
		void waitForThreads(Object^ paramObj);

		static int* m_chessBoard = new int[64];
		static bool m_knightTourFinded = false;
		static Object^ Lock = gcnew Object;
	private:
		double* m_firstMap;
		double* m_secondMap;
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
	
		WrapperPixels scramblingPixels(int i, int j, int k);
		WrapperPixels descramblingPixels(int i, int j, int k);

	private:
		ScramblingImage* obj;
	};
}
