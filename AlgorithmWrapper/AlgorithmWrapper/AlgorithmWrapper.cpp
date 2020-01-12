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

bool AlgorithmWrapper::WrapperClass::loadImage(char* path)
{
	return obj->loadImage(path);
}

void AlgorithmWrapper::WrapperClass::saveImage(char* path)
{
	obj->saveImage(path);
}

void AlgorithmWrapper::WrapperClass::generateKeys(double* key1, double* key2)
{
	ThreadX^ o1 = gcnew ThreadX(obj);
	ThreadX^ o2 = gcnew ThreadX(obj);
	ThreadX^ o3 = gcnew ThreadX(obj);
	ThreadX^ o4 = gcnew ThreadX(obj);
	ThreadX^ o5 = gcnew ThreadX(obj);

	Thread^ secondThread = gcnew Thread(gcnew ParameterizedThreadStart(o2, &ThreadX::knightTravel));
	Thread^ thirdThread = gcnew Thread(gcnew ParameterizedThreadStart(o3, &ThreadX::knightTravel));
	Thread^ fourthThread = gcnew Thread(gcnew ParameterizedThreadStart(o4, &ThreadX::knightTravel));
	Thread^ fifthThread = gcnew Thread(gcnew ParameterizedThreadStart(o5, &ThreadX::knightTravel));

	ThreadParams^ param = gcnew ThreadParams(secondThread, thirdThread, fourthThread, fifthThread);
	Thread^ firstThread = gcnew Thread(gcnew ParameterizedThreadStart(o1, &ThreadX::waitForThreads));
	ThreadX::m_knightTourFinded = false;

	firstThread->Start(param);

	ThreadParamsPos^ positions = gcnew ThreadParamsPos(0, 0);
	Random^ random = gcnew Random();

	positions->x = 0;
	positions->y = random->Next(0, 7);
	secondThread->Start(positions);

	positions->x = random->Next(0, 7);
	positions->y = 0;
	thirdThread->Start(positions);

	positions->x = random->Next(0, 7);
	positions->y = random->Next(0, 7);
	fourthThread->Start(positions);

	positions->x = 0;
	positions->y = 0;
	fifthThread->Start(positions);

	firstThread->Join();

	obj->generateKeys(*key1, *key2, ThreadX::m_chessBoard);
}

void AlgorithmWrapper::WrapperClass::preparingRules(double key1, double key2)
{
	ThreadX^ o1 = gcnew ThreadX(obj);
	ThreadX^ o2 = gcnew ThreadX(obj);

	Thread^ firstThread = gcnew Thread(gcnew ParameterizedThreadStart(o1, &ThreadX::calculateFirstChaoticMap));
	Thread^ secondThread = gcnew Thread(gcnew ParameterizedThreadStart(o2, &ThreadX::calculateSecondChaoticMap));

	firstThread->Start(key1);
	secondThread->Start(key2);

	firstThread->Join();
	secondThread->Join();

	obj->generatePRBG(o1->getFirstMap(), o2->getSecondMap());

	firstThread = gcnew Thread(gcnew ThreadStart(o1, &ThreadX::calculateRulesKMR));
	secondThread = gcnew Thread(gcnew ThreadStart(o2, &ThreadX::calculateRulesRNS));

	firstThread->Start();
	secondThread->Start();

	firstThread->Join();
	secondThread->Join();
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

AlgorithmWrapper::WrapperPixels AlgorithmWrapper::WrapperClass::threadScramblingPixels(int i, int j, int k, int startX, int endX, int startY, int endY)
{
	Pixel* tempPixels = obj->threadScramblingPixels(i, j, k, startX, endX, startY, endY);

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

AlgorithmWrapper::WrapperPixels AlgorithmWrapper::WrapperClass::threadDescramblingPixels(int i, int j, int k, int startX, int endX, int startY, int endY)
{
	Pixel* tempPixels = obj->threadDescramblingPixels(i, j, k, startX, endX, startY, endY);

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

AlgorithmWrapper::ThreadX::ThreadX(ScramblingImage* obj)
{
	m_obj = obj;
}

void AlgorithmWrapper::ThreadX::calculateFirstChaoticMap(Object^ data)
{
	m_firstMap = m_obj->computeChaoticMap((double)data, 0);
}

void AlgorithmWrapper::ThreadX::calculateSecondChaoticMap(Object ^ data)
{
	m_secondMap = m_obj->computeChaoticMap((double)data, 1);
}

void AlgorithmWrapper::ThreadX::calculateRulesKMR()
{
	m_obj->computeRulesKMR();
}

void AlgorithmWrapper::ThreadX::calculateRulesRNS()
{
	m_obj->computeRulesRNS();
}

double * AlgorithmWrapper::ThreadX::getFirstMap()
{
	return m_firstMap;
}

double * AlgorithmWrapper::ThreadX::getSecondMap()
{
	return m_secondMap;
}

void AlgorithmWrapper::ThreadX::knightTravel(Object^ paramObj)
{
	ThreadParamsPos^ param = (ThreadParamsPos^)paramObj;

	int chessBoard[8][8] = { 0 };

	m_obj->knightTravel(param->x, param->y, 0, chessBoard);
	
	Monitor::Enter(Lock);
	if (!m_knightTourFinded) {
		m_knightTourFinded = true;
		for (int i = 0; i < 8; i++)
		{
			for (int j = 0; j < 8; j++)
			{
				m_chessBoard[i * 8 + j] = chessBoard[i][j];
			}
		}
	}
	Monitor::Exit(Lock);
}

void AlgorithmWrapper::ThreadX::waitForThreads(Object^ paramObj)
{
	ThreadParams^ param = (ThreadParams^)paramObj;
	int count = 0;

	while (true) {
		Monitor::Enter(Lock);
		if (m_knightTourFinded || count > 6)
		{
				param->myFirst->Abort();
				param->mySecond->Abort();
				param->myThird->Abort();
				param->myFourth->Abort();

			Monitor::Exit(Lock);
			return;
		}
		Monitor::Exit(Lock);
		count++;
		Thread::Sleep(1000);
	}
}
