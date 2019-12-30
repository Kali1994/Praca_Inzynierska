#pragma once

#include "stdafx.h"
#include "ScramblingImage.h"


ScramblingImage::ScramblingImage(string sName,string sResult)
{
	//m_piENC = new Encryption(m_piPicture);
	//m_piPRGB = new PRBG(m_piPicture->iGetSize());

	m_test = 0;
}

void ScramblingImage::vRunScrambling()
{
	/*
	m_piPRGB->vGeneratePRBG();
	m_piPRGB->vComputingKMRandRNS();
	
	uint8_t* AImage;
	uint8_t* BImage;


	cout << "Szyfrowanie" << endl;

	for (int k = 0; k < 3; k++)
	{
		for (int i = 0; i < m_piPicture->iGetSize(); i++)
		{
			for (int j = 0; j < m_piPicture->iGetSize(); j++)
			{
				m_piENC->vGenerateKnightRules(m_piPRGB->u8GetValueKMR(i,j,k),m_piPRGB->u8GetValueRNS(i,j,k));
				
				AImage = m_piENC->u8ComputingLimitsOnScrambling(i, j, 0);
				BImage = m_piENC->u8ComputingLimitsOnScrambling(i, j, 1);

				m_piENC->vTransposingPixelColor(AImage,BImage,i,j,k);
			}
			cout << i << endl;
		}
	}
	
	cout << "Szyfrowanie Zakonczone" << endl;

	m_piPicture->vWriteToImage();
	m_piPicture->vSaveImage();
	*/
}

void ScramblingImage::vRunDescrambling()
{
	/*
	m_piPRGB->vGeneratePRBG();
	m_piPRGB->vComputingKMRandRNS();

	uint8_t* AImage;
	uint8_t* BImage;

	cout << "DeSzyfracja" << endl;

	for (int k = 3; k >= 0; k--)
	{
		for (int i = m_piPicture->iGetSize() - 1; i >= 0; i--)
		{
			for (int j = m_piPicture->iGetSize() - 1; j >= 0; j--)
			{
			
				m_piENC->vGenerateKnightRules(m_piPRGB->u8GetValueKMR(i, j, k), m_piPRGB->u8GetValueRNS(i, j, k));

				AImage = m_piENC->u8ComputingLimitsOnScrambling(i, j, 0);
				BImage = m_piENC->u8ComputingLimitsOnScrambling(i, j, 1);

				m_piENC->vTransposingPixelColorDescrambling(AImage, BImage,i, j, k);
			}
			cout << i << endl;
		}
	}

	m_piPicture->vWriteToImage();
	m_piPicture->vSaveImage();

	cout << "Deszyfrowanie Zakonczone" << endl;
	*/
}

int ScramblingImage::getTest()
{
	return m_test;
}

void ScramblingImage::setTest(int value)
{
	m_test = value;
}

void ScramblingImage::loadImage(std::string path)
{
	m_picture.loadImage(path);
}

void ScramblingImage::saveImage(std::string path)
{
	m_picture.saveImage(path);
}

ScramblingImage::~ScramblingImage()
{
	//delete m_piPRGB;
	//delete m_piENC;
}
