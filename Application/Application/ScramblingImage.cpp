#pragma once

#include "stdafx.h"
#include "ScramblingImage.h"


ScramblingImage::ScramblingImage() :
	m_encryption(&m_picture)
{

}

Pixel* ScramblingImage::scramblingPixels(int i, int j, int k)
{
	uint8_t* AImage;
	uint8_t* BImage;

	static Pixel pixels[3];
	pixels[0].posX = i;
	pixels[0].posY = j;

	m_encryption.vGenerateKnightRules(m_scrambler.getValueKMR(i, j, k), m_scrambler.getValueRNS(i, j, k));
				
	AImage = m_encryption.computingLimitsOnScrambling(i, j, 0, pixels[1]);
	BImage = m_encryption.computingLimitsOnScrambling(i, j, 1, pixels[2]);

	m_encryption.transposingPixelColor(AImage, BImage, i, j, k);
	setPixelsValue(pixels);

	return pixels;
}

Pixel* ScramblingImage::descramblingPixels(int i, int j, int k)
{
	uint8_t* AImage;
	uint8_t* BImage;

	static Pixel pixels[3];
	pixels[0].posX = i;
	pixels[0].posY = j;

	m_encryption.vGenerateKnightRules(m_scrambler.getValueKMR(i, j, k), m_scrambler.getValueRNS(i, j, k));

	AImage = m_encryption.computingLimitsOnScrambling(i, j, 0, pixels[1]);
	BImage = m_encryption.computingLimitsOnScrambling(i, j, 1, pixels[2]);

	m_encryption.transposingPixelColorDescrambling(AImage, BImage, i, j, k);
	setPixelsValue(pixels);

	return pixels;
}

void ScramblingImage::loadImage(std::string path)
{
	m_picture.loadImage(path);
}

void ScramblingImage::saveImage(std::string path)
{
	m_picture.saveImage(path);
}

void ScramblingImage::generateKeys(double& key1, double& key2)
{
	key1 = 0.687754925117; //m_generatorKey.generateKey();
	key2 = -0.013462335467; //m_generatorKey.generateKey();
}

void ScramblingImage::preparingRules(double key1, double key2)
{
	m_scrambler.generatePRBG(key1, key2, m_picture.getRows(), m_picture.getColumns());
	m_scrambler.computingKMRandRNS();
}

ScramblingImage::~ScramblingImage()
{
	//delete m_piPRGB;
	//delete m_piENC;
}

void ScramblingImage::setPixelsValue(Pixel* pixels)
{
	uint8_t* first = m_picture.getRGBValueOfPixel(pixels[0].posX, pixels[0].posY);
	uint8_t* second = m_picture.getRGBValueOfPixel(pixels[1].posX, pixels[1].posY);
	uint8_t* third = m_picture.getRGBValueOfPixel(pixels[2].posX, pixels[2].posY);

	pixels[0].setValue(first);
	pixels[1].setValue(second);
	pixels[2].setValue(third);
}
