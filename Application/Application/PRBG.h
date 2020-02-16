#pragma once
#include "stdafx.h"
#include "BitOperation.h"

class PRBG :public BitOperation
{
public:
	PRBG();
	~PRBG();

	void computeRulesKMR();
	void computeRulesRNS();
	void deallocateRules();

	void generatePRBG(double, double);
	void setNumberGenerateBits(int rows, int columns);

	uint8_t getValueKMR(int, int, int);
	uint8_t getValueRNS(int, int, int);

protected:
	int m_rows;
	int m_columns;
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

	int m_numberGenerateBits;
};