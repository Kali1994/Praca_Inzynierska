#include "stdafx.h"
#include "Encryption.h"


Encryption::Encryption(Picture* piPicture): m_piPicture(piPicture)
{

}

Encryption::~Encryption()
{

}

uint8_t* Encryption::u8ComputingLimitsOnScrambling(int i, int j, int iRules)
{

	if (iRules != 0)
	{
		i = m_iOldi;
		j = m_iOldj;
	}

	switch (m_iArrRules[iRules][0])
	{
		case 0:
			i = i - 2 * m_iArrRules[iRules][1];	 j = j + 1 * m_iArrRules[iRules][1]; break;
		case 1:
			i = i - 1 * m_iArrRules[iRules][1];	 j = j + 2 * m_iArrRules[iRules][1]; break;
		case 2:
			i = i + 1 * m_iArrRules[iRules][1];	 j = j + 2 * m_iArrRules[iRules][1]; break;
		case 3:
			i = i + 2 * m_iArrRules[iRules][1];  j = j + 1 * m_iArrRules[iRules][1]; break;
		case 4:
			i = i + 2 * m_iArrRules[iRules][1];  j = j - 1 * m_iArrRules[iRules][1]; break;
		case 5:
			i = i + 1 * m_iArrRules[iRules][1];	 j = j - 2 * m_iArrRules[iRules][1]; break;
		case 6:
			i = i - 1 * m_iArrRules[iRules][1];	 j = j - 2 * m_iArrRules[iRules][1]; break;
		default:
			i = i - 2 * m_iArrRules[iRules][1];	 j = j - 1 * m_iArrRules[iRules][1]; break;

	}

	if (i < 0)
	{
		i = i + 512;
	}
	if(i >= m_piPicture->iGetSize())
	{
		i = (i % m_piPicture->iGetSize()-1) + 1;
	}

	if (j < 0)
	{
		j = j + 512;
	}
	if (j >= m_piPicture->iGetSize())
	{
		j = (j % m_piPicture->iGetSize()-1) + 1;
	}

	if (iRules == 0)
	{
		m_iOldi = i;
		m_iOldj = j;
	}
	
	return m_piPicture->u8GetRGBValueOfPixel(i, j);
}

void Encryption::vTransposingPixelColor(uint8_t* piATable,uint8_t* piBTable, int i, int j, int k)
{
	uint8_t Pixel = m_piPicture->u8GetValueOfPixel(i,j,k);

	if (this->m_iArrRules[0][2] == 0 && this->m_iArrRules[1][2] == 0 && k == 0)
	{
		m_piPicture->vSetValueofPixel(i, j, k, piATable[k + 1]);
		piATable[k + 1] = piBTable[k + 2]; piBTable[k + 2] = Pixel;
	}
	else if (this->m_iArrRules[0][2] == 0 && this->m_iArrRules[1][2] == 1 && k == 0)
	{
		m_piPicture->vSetValueofPixel(i, j, k, piBTable[k + 2]);
		piBTable[k + 2] = Pixel; 
	}
	else if (this->m_iArrRules[0][2] == 1 && this->m_iArrRules[1][2] == 0 && k == 0)
	{
		m_piPicture->vSetValueofPixel(i, j, k, piATable[k + 2]);
		piATable[k + 2] = piBTable[k + 1]; piBTable[k + 1] = Pixel; 
	}
	else if (this->m_iArrRules[0][2] == 1 && this->m_iArrRules[1][2] == 1 && k == 0)
	{
		m_piPicture->vSetValueofPixel(i, j, k, piBTable[k + 1]);
		piBTable[k + 1] = Pixel; 
	}
	else if (this->m_iArrRules[0][2] == 0 && this->m_iArrRules[1][2] == 0 && k == 1)
	{
		m_piPicture->vSetValueofPixel(i, j, k, piBTable[k - 1]);
		piBTable[k - 1] = Pixel;
	}
	else if (this->m_iArrRules[0][2] == 0 && this->m_iArrRules[1][2] == 1 && k == 1)
	{
		m_piPicture->vSetValueofPixel(i, j, k, piATable[k - 1]);
		piATable[k - 1] = piBTable[k + 1]; piBTable[k + 1] = Pixel;
	}
	else if (this->m_iArrRules[0][2] == 1 && this->m_iArrRules[1][2] == 0 && k == 1)
	{
		m_piPicture->vSetValueofPixel(i, j, k, piATable[k + 1]);
		piATable[k + 1] = Pixel;
	}
	else if (this->m_iArrRules[0][2] == 1 && this->m_iArrRules[1][2] == 1 && k == 1)
	{
		m_piPicture->vSetValueofPixel(i, j, k, piATable[k + 1]);
		piATable[k + 1] = piBTable[k - 1]; piBTable[k - 1] = Pixel;
	}
	else if (this->m_iArrRules[0][2] == 0 && this->m_iArrRules[1][2] == 0 && k == 2)
	{
		m_piPicture->vSetValueofPixel(i, j, k, piATable[k - 1]);
		piATable[k - 1] = piBTable[k - 2]; piBTable[k - 2] = Pixel; 
	}
	else if (this->m_iArrRules[0][2] == 0 && this->m_iArrRules[1][2] == 1 && k == 2)
	{
		m_piPicture->vSetValueofPixel(i, j, k, piATable[k - 2]);
		piATable[k - 2] = piBTable[k - 1]; piBTable[k - 1] = Pixel; 
	}
	else if (this->m_iArrRules[0][2] == 1 && this->m_iArrRules[1][2] == 0 && k == 2)
	{
		m_piPicture->vSetValueofPixel(i, j, k, piATable[k - 2]);
		piATable[k - 2] = Pixel;  
	}
	else if (this->m_iArrRules[0][2] == 1 && this->m_iArrRules[1][2] == 1 && k == 2)
	{
		m_piPicture->vSetValueofPixel(i, j, k, piBTable[k - 1]);
		piBTable[k - 1] = Pixel;
	}
}

void Encryption::vTransposingPixelColorDescrambling(uint8_t* piATable, uint8_t* piBTable, int i, int j, int k)
{
	uint8_t Pixel = m_piPicture->u8GetValueOfPixel(i, j, k);

	if (this->m_iArrRules[0][2] == 0 && this->m_iArrRules[1][2] == 0 && k == 0)
	{
		m_piPicture->vSetValueofPixel(i, j, k, piBTable[k + 2]);
		piBTable[k + 2] = piATable[k + 1]; piATable[k + 1] = Pixel;
	}
	else if (this->m_iArrRules[0][2] == 0 && this->m_iArrRules[1][2] == 1 && k == 0)
	{
		m_piPicture->vSetValueofPixel(i, j, k, piBTable[k + 2]);
		piBTable[k + 2] = Pixel;
	}
	else if (this->m_iArrRules[0][2] == 1 && this->m_iArrRules[1][2] == 0 && k == 0)
	{
		m_piPicture->vSetValueofPixel(i, j, k, piBTable[k + 1]);
		piBTable[k + 1] = piATable[k + 2]; piATable[k + 2] = Pixel;
	}
	else if (this->m_iArrRules[0][2] == 1 && this->m_iArrRules[1][2] == 1 && k == 0)
	{
		m_piPicture->vSetValueofPixel(i, j, k, piBTable[k + 1]);
		piBTable[k + 1] = Pixel;
	}
	else if (this->m_iArrRules[0][2] == 0 && this->m_iArrRules[1][2] == 0 && k == 1)
	{
		m_piPicture->vSetValueofPixel(i, j, k, piBTable[k - 1]);
		piBTable[k - 1] = Pixel;
	}
	else if (this->m_iArrRules[0][2] == 0 && this->m_iArrRules[1][2] == 1 && k == 1)
	{
		m_piPicture->vSetValueofPixel(i, j, k, piBTable[k + 1]);
		piBTable[k + 1] = piATable[k - 1]; piATable[k - 1] = Pixel;
	}
	else if (this->m_iArrRules[0][2] == 1 && this->m_iArrRules[1][2] == 0 && k == 1)
	{
		m_piPicture->vSetValueofPixel(i, j, k, piATable[k + 1]);
		piATable[k + 1] = Pixel;
	}
	else if (this->m_iArrRules[0][2] == 1 && this->m_iArrRules[1][2] == 1 && k == 1)
	{
		m_piPicture->vSetValueofPixel(i, j, k, piBTable[k - 1]);
		piBTable[k - 1] = piATable[k + 1]; piATable[k + 1] = Pixel;
	}
	else if (this->m_iArrRules[0][2] == 0 && this->m_iArrRules[1][2] == 0 && k == 2)
	{
		m_piPicture->vSetValueofPixel(i, j, k, piBTable[k - 2]);
		piBTable[k - 2] = piATable[k - 1]; piATable[k - 1] = Pixel;
	}
	else if (this->m_iArrRules[0][2] == 0 && this->m_iArrRules[1][2] == 1 && k == 2)
	{
		m_piPicture->vSetValueofPixel(i, j, k, piBTable[k - 1]);
		piBTable[k - 1] = piATable[k - 2]; piATable[k - 2] = Pixel;
	}
	else if (this->m_iArrRules[0][2] == 1 && this->m_iArrRules[1][2] == 0 && k == 2)
	{
		m_piPicture->vSetValueofPixel(i, j, k, piATable[k - 2]);
		piATable[k - 2] = Pixel;
	}
	else if (this->m_iArrRules[0][2] == 1 && this->m_iArrRules[1][2] == 1 && k == 2)
	{
		m_piPicture->vSetValueofPixel(i, j, k, piBTable[k - 1]);
		piBTable[k - 1] = Pixel;
	}

}