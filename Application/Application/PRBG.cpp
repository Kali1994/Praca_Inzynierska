#pragma once
#include "stdafx.h"
#include "PRBG.h"

PRBG::PRBG() :
	m_KMR(nullptr),
	m_RNS(nullptr),
	m_r1(5.938725025421),
	m_r2(1.237490188615),
	m_numberGenerateBits(0)
{

}

PRBG::~PRBG()
{
	if (m_KMR != nullptr)
	{
		deallocateMemory(m_KMR, m_rows, m_columns);
	}

	if (m_RNS != nullptr)
	{
		deallocateMemory(m_RNS, m_rows, m_columns);
	}
}

uint8_t PRBG::getValueKMR(int i, int j, int k)
{
	return m_KMR[i][j][k];
}

uint8_t PRBG::getValueRNS(int i, int j, int k)
{
	return m_RNS[i][j][k];
}

void PRBG::computeRulesKMR()
{
	char cByteA[8];

	m_iFile1.open("Bits_A.txt");

	m_KMR = allocateMemory(m_KMR, m_rows, m_columns);

	for (int i = 0; i < m_rows; i++)
	{
		for (int j = 0; j < m_columns; j++)
		{
			for (int k = 0; k < 3; k++)
			{
				m_iFile1.read(cByteA, BitOperation::m_numberBits);
				m_KMR[i][j][k] = BitOperation::iBin2Dec(cByteA, 1, BitOperation::m_numberBits);
			}
		}
	}

	m_iFile1.close();
}

void PRBG::computeRulesRNS()
{
	char cByteB[8];

	m_iFile2.open("Bits_B.txt");

	m_RNS = allocateMemory(m_RNS, m_rows, m_columns);

	for (int i = 0; i < m_rows; i++)
	{
		for (int j = 0; j < m_columns; j++)
		{
			for (int k = 0; k < 3; k++)
			{
				m_iFile2.read(cByteB, BitOperation::m_numberBits);
				m_RNS[i][j][k] = BitOperation::iBin2Dec(cByteB, 1, BitOperation::m_numberBits);
			}
		}
	}

	m_iFile2.close();
}

void PRBG::deallocateRules()
{
	if (m_KMR != nullptr)
	{
		deallocateMemory(m_KMR, m_rows, m_columns);
		m_KMR = nullptr;
	}

	if (m_RNS != nullptr)
	{
		deallocateMemory(m_RNS, m_rows, m_columns);
		m_RNS = nullptr;
	}
}

void PRBG::generatePRBG(double keyFirst, double keySecond)
{
	double Ft;

	m_oFile1.open("Bits_A.txt");
	m_oFile2.open("Bits_B.txt");

	for (int i = 0; i < m_numberGenerateBits; i++)
	{
		keyFirst = 2 / M_PI * atan(cos(keyFirst * m_r1) / sin(keyFirst * m_r1));
		keySecond = 2 / M_PI * atan(cos(keySecond * m_r2) / sin(keySecond * m_r2));

		Ft = (keyFirst + keySecond) / (1 - keyFirst * keySecond);
		Ft = modf(Ft, &Ft);
		Ft *= 100;

		if (Ft < 0)
		{
			Ft = abs(Ft);
		}

		if (Ft > 0 && Ft <= 25)
		{
			m_oFile1 << 0;
			m_oFile2 << 0;
		}
		else if (Ft > 25 && Ft <= 50)
		{
			m_oFile1 << 0;
			m_oFile2 << 1;
		}
		else if (Ft > 50 && Ft <= 75)
		{
			m_oFile1 << 1;
			m_oFile2 << 0;
		}
		else if (Ft > 75 && Ft <= 100)
		{
			m_oFile1 << 1;
			m_oFile2 << 1;
		}
	}

	m_oFile1.close();
	m_oFile2.close();
}

void PRBG::setNumberGenerateBits(int rows, int columns)
{
	m_numberGenerateBits = rows * columns * m_numberBits * 3;
	m_rows = rows;
	m_columns = columns;
}