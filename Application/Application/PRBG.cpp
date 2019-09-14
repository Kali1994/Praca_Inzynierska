#include "stdafx.h"
#include "PRBG.h"



PRBG::PRBG(int iSize)
{
	this->m_iSize = iSize;

	m_KMR = piu8AllocateMemory(m_KMR, iSize);
	m_RNS = piu8AllocateMemory(m_RNS, iSize);
}

void PRBG::vGeneratePRBG()
{
	system("cls");
	cout << "Generowanie plikow Bits_A.txt i Bits_B.txt" << endl;

	m_oFile1.open("Bits_A.txt");
	m_oFile2.open("Bits_B.txt");

	m_x0 = 0.687754925117;
	m_x1 = -0.013462335467;
	m_r1 = 5.938725025421;
	m_r2 = 1.237490188615;

	double tempF1, tempF2;
	double Ft = 0;

	for (int i = 0; i<m_iSize; i++)
	{
		for (int j = 0; j<m_iSize; j++)
		{
			for (int k = 0; k<3; k++)
			{
				for (int b = 0; b<8; b++)
				{
					tempF1 = 2 / M_PI * atan(cos(m_r1 * m_x0)/ sin(m_r1 * m_x0));
					tempF2 = 2 / M_PI * atan(cos(m_r2 * m_x1)/ sin(m_r2 * m_x1));
					m_x0 = tempF1;
					m_x1 = tempF2;

					Ft = (tempF1 + tempF2) / (1 - tempF1 * tempF2);
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
			}
		}
	}

	m_oFile1.close();
	m_oFile2.close();
}


void PRBG::vComputingKMRandRNS()
{

	cout << "Tworzenie Regul" << endl;

	char cByteA[8], cByteB[8];

	m_iFile1.open("Bits_A.txt");
	m_iFile2.open("Bits_B.txt");

	this->vInitializeBothTable();

	int jeden = 0;
	int zero = 0;

	for (int i = 0; i < m_iSize; i++)
	{
		for (int j = 0; j < m_iSize; j++)
		{
			for (int k = 0; k < 3; k++)
			{
				m_iFile1.read(cByteA, BitOperation::m_iNumberBits);
				m_iFile2.read(cByteB, BitOperation::m_iNumberBits);

				for (int i = 0; i < 8; i++)
				{
					if ('1' == cByteA[i])
					{
						jeden++;
					}
					else if ('0' == cByteA[i])
					{
						zero++;
					}
					else
					{
						cout << "DASDASDSADAS";
					}
				}

				m_KMR[i][j][k] = BitOperation::iBin2Dec(cByteA,1, BitOperation::m_iNumberBits);
				m_RNS[i][j][k] = BitOperation::iBin2Dec(cByteB,1, BitOperation::m_iNumberBits);
			}

		}
	}

	cout << "!!! jeden  = " << jeden;
	cout << "!!! zero = " << zero;
	m_iFile1.close();
	m_iFile2.close();
}

PRBG::~PRBG()
{

}

uint8_t PRBG::u8GetValueKMR(int i, int j, int k)
{
	return m_KMR[i][j][k];
}

uint8_t PRBG::u8GetValueRNS(int i, int j, int k)
{
	return m_RNS[i][j][k];
}

void PRBG::vInitializeBothTable()
{
	for (int i = 0; i < m_iSize; i++)
	{
		for (int j = 0; j < m_iSize; j++)
		{
			m_KMR[i][j][0] = 0;
			m_KMR[i][j][1] = 0;
			m_KMR[i][j][2] = 0;

			m_RNS[i][j][0] = 0;
			m_RNS[i][j][1] = 0;
			m_RNS[i][j][2] = 0;
		}
	}

}