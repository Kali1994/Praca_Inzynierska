#pragma once
#include "stdafx.h"
#include "Encryption.h"


Encryption::Encryption(Picture* piPicture): m_picture(piPicture)
{

}

Encryption::~Encryption()
{

}

uint8_t* Encryption::computingLimitsOnScrambling(int i, int j, int iRules, Pixel& pixel)
{
	if (iRules != 0)
	{
		i = m_Oldi;
		j = m_Oldj;
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
	if (i >= m_picture->getRows())
	{
		i = (i % m_picture->getRows() - 1) + 1;
	}

	if (j < 0)
	{
		j = j + 512;
	}
	if (j >= m_picture->getColumns())
	{
		j = (j % m_picture->getColumns() - 1) + 1;
	}

	if (iRules == 0)
	{
		m_Oldi = i;
		m_Oldj = j;
	}

	pixel.posX = i;
	pixel.posY = j;
	return m_picture->getRGBValueOfPixel(i, j);
}

uint8_t* Encryption::threadComputingLimitsOnScrambling(int i, int j, int iRules, Pixel& pixel, DividedImage& image)
{

	if (iRules != 0)
	{
		i = m_Oldi;
		j = m_Oldj;
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

	while (i < image.m_startX)
	{
		i = i + (image.m_endX - image.m_startX);
	}
	if (i >= image.m_endX)
	{
		i = (i % image.m_endX);

		if (i < image.m_startX) {
			i = i + (image.m_endX - image.m_startX);
		}
	}

	while (j < image.m_startY)
	{
		j = j + (image.m_endY - image.m_startY);
	}
	if (j >= image.m_endY)
	{
		j = (j % image.m_endY);

		if (j < image.m_startY) {
			j = j + (image.m_endY - image.m_startY);
		}
	}

	if (iRules == 0)
	{
		m_Oldi = i;
		m_Oldj = j;
	}

	pixel.posX = i;
	pixel.posY = j;
	return m_picture->getRGBValueOfPixel(i, j);
}

void Encryption::transposingPixelColor(uint8_t* piATable,uint8_t* piBTable, int i, int j, int k)
{
	uint8_t Pixel = m_picture->getValueOfPixel(i,j,k);

	if (this->m_iArrRules[0][2] == 0 && this->m_iArrRules[1][2] == 0 && k == 0)
	{
		m_picture->setValueofPixel(i, j, k, piATable[k + 1]);
		piATable[k + 1] = piBTable[k + 2]; piBTable[k + 2] = Pixel;
	}
	else if (this->m_iArrRules[0][2] == 0 && this->m_iArrRules[1][2] == 1 && k == 0)
	{
		m_picture->setValueofPixel(i, j, k, piBTable[k + 2]);
		piBTable[k + 2] = Pixel; 
	}
	else if (this->m_iArrRules[0][2] == 1 && this->m_iArrRules[1][2] == 0 && k == 0)
	{
		m_picture->setValueofPixel(i, j, k, piATable[k + 2]);
		piATable[k + 2] = piBTable[k + 1]; piBTable[k + 1] = Pixel; 
	}
	else if (this->m_iArrRules[0][2] == 1 && this->m_iArrRules[1][2] == 1 && k == 0)
	{
		m_picture->setValueofPixel(i, j, k, piBTable[k + 1]);
		piBTable[k + 1] = Pixel; 
	}
	else if (this->m_iArrRules[0][2] == 0 && this->m_iArrRules[1][2] == 0 && k == 1)
	{
		m_picture->setValueofPixel(i, j, k, piBTable[k - 1]);
		piBTable[k - 1] = Pixel;
	}
	else if (this->m_iArrRules[0][2] == 0 && this->m_iArrRules[1][2] == 1 && k == 1)
	{
		m_picture->setValueofPixel(i, j, k, piATable[k - 1]);
		piATable[k - 1] = piBTable[k + 1]; piBTable[k + 1] = Pixel;
	}
	else if (this->m_iArrRules[0][2] == 1 && this->m_iArrRules[1][2] == 0 && k == 1)
	{
		m_picture->setValueofPixel(i, j, k, piATable[k + 1]);
		piATable[k + 1] = Pixel;
	}
	else if (this->m_iArrRules[0][2] == 1 && this->m_iArrRules[1][2] == 1 && k == 1)
	{
		m_picture->setValueofPixel(i, j, k, piATable[k + 1]);
		piATable[k + 1] = piBTable[k - 1]; piBTable[k - 1] = Pixel;
	}
	else if (this->m_iArrRules[0][2] == 0 && this->m_iArrRules[1][2] == 0 && k == 2)
	{
		m_picture->setValueofPixel(i, j, k, piATable[k - 1]);
		piATable[k - 1] = piBTable[k - 2]; piBTable[k - 2] = Pixel; 
	}
	else if (this->m_iArrRules[0][2] == 0 && this->m_iArrRules[1][2] == 1 && k == 2)
	{
		m_picture->setValueofPixel(i, j, k, piATable[k - 2]);
		piATable[k - 2] = piBTable[k - 1]; piBTable[k - 1] = Pixel; 
	}
	else if (this->m_iArrRules[0][2] == 1 && this->m_iArrRules[1][2] == 0 && k == 2)
	{
		m_picture->setValueofPixel(i, j, k, piATable[k - 2]);
		piATable[k - 2] = Pixel;  
	}
	else if (this->m_iArrRules[0][2] == 1 && this->m_iArrRules[1][2] == 1 && k == 2)
	{
		m_picture->setValueofPixel(i, j, k, piBTable[k - 1]);
		piBTable[k - 1] = Pixel;
	}
}

void Encryption::transposingPixelColorDescrambling(uint8_t* piATable, uint8_t* piBTable, int i, int j, int k)
{
	uint8_t Pixel = m_picture->getValueOfPixel(i, j, k);

	if (this->m_iArrRules[0][2] == 0 && this->m_iArrRules[1][2] == 0 && k == 0)
	{
		m_picture->setValueofPixel(i, j, k, piBTable[k + 2]);
		piBTable[k + 2] = piATable[k + 1]; piATable[k + 1] = Pixel;
	}
	else if (this->m_iArrRules[0][2] == 0 && this->m_iArrRules[1][2] == 1 && k == 0)
	{
		m_picture->setValueofPixel(i, j, k, piBTable[k + 2]);
		piBTable[k + 2] = Pixel;
	}
	else if (this->m_iArrRules[0][2] == 1 && this->m_iArrRules[1][2] == 0 && k == 0)
	{
		m_picture->setValueofPixel(i, j, k, piBTable[k + 1]);
		piBTable[k + 1] = piATable[k + 2]; piATable[k + 2] = Pixel;
	}
	else if (this->m_iArrRules[0][2] == 1 && this->m_iArrRules[1][2] == 1 && k == 0)
	{
		m_picture->setValueofPixel(i, j, k, piBTable[k + 1]);
		piBTable[k + 1] = Pixel;
	}
	else if (this->m_iArrRules[0][2] == 0 && this->m_iArrRules[1][2] == 0 && k == 1)
	{
		m_picture->setValueofPixel(i, j, k, piBTable[k - 1]);
		piBTable[k - 1] = Pixel;
	}
	else if (this->m_iArrRules[0][2] == 0 && this->m_iArrRules[1][2] == 1 && k == 1)
	{
		m_picture->setValueofPixel(i, j, k, piBTable[k + 1]);
		piBTable[k + 1] = piATable[k - 1]; piATable[k - 1] = Pixel;
	}
	else if (this->m_iArrRules[0][2] == 1 && this->m_iArrRules[1][2] == 0 && k == 1)
	{
		m_picture->setValueofPixel(i, j, k, piATable[k + 1]);
		piATable[k + 1] = Pixel;
	}
	else if (this->m_iArrRules[0][2] == 1 && this->m_iArrRules[1][2] == 1 && k == 1)
	{
		m_picture->setValueofPixel(i, j, k, piBTable[k - 1]);
		piBTable[k - 1] = piATable[k + 1]; piATable[k + 1] = Pixel;
	}
	else if (this->m_iArrRules[0][2] == 0 && this->m_iArrRules[1][2] == 0 && k == 2)
	{
		m_picture->setValueofPixel(i, j, k, piBTable[k - 2]);
		piBTable[k - 2] = piATable[k - 1]; piATable[k - 1] = Pixel;
	}
	else if (this->m_iArrRules[0][2] == 0 && this->m_iArrRules[1][2] == 1 && k == 2)
	{
		m_picture->setValueofPixel(i, j, k, piBTable[k - 1]);
		piBTable[k - 1] = piATable[k - 2]; piATable[k - 2] = Pixel;
	}
	else if (this->m_iArrRules[0][2] == 1 && this->m_iArrRules[1][2] == 0 && k == 2)
	{
		m_picture->setValueofPixel(i, j, k, piATable[k - 2]);
		piATable[k - 2] = Pixel;
	}
	else if (this->m_iArrRules[0][2] == 1 && this->m_iArrRules[1][2] == 1 && k == 2)
	{
		m_picture->setValueofPixel(i, j, k, piBTable[k - 1]);
		piBTable[k - 1] = Pixel;
	}

}
