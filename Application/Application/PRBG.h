#pragma once
#include "stdafx.h"
#include "BitOperation.h"

class PRBG :public BitOperation
{
public:
	PRBG(){}
	PRBG(int);

	void vGeneratePRBG();
	void vComputingKMRandRNS();

	uint8_t u8GetValueKMR(int, int, int);
	uint8_t u8GetValueRNS(int, int, int);

	~PRBG();
protected:
	int m_iSize;
private:
	ofstream m_oFile1;
	ofstream m_oFile2;

	ifstream m_iFile1;
	ifstream m_iFile2;;

	uint8_t*** m_KMR;
	uint8_t*** m_RNS;

	long double m_x0;
	long double m_x1;
	long double m_r1;
	long double m_r2;

	void vInitializeBothTable();
};