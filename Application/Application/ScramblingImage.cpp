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

	m_encryption.generateKnightRules(m_scrambler.getValueKMR(i, j, k), m_scrambler.getValueRNS(i, j, k));

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

	m_encryption.generateKnightRules(m_scrambler.getValueKMR(i, j, k), m_scrambler.getValueRNS(i, j, k));

	AImage = m_encryption.computingLimitsOnScrambling(i, j, 0, pixels[1]);
	BImage = m_encryption.computingLimitsOnScrambling(i, j, 1, pixels[2]);

	m_encryption.transposingPixelColorDescrambling(AImage, BImage, i, j, k);
	setPixelsValue(pixels);

	return pixels;
}

Pixel* ScramblingImage::threadScramblingPixels(int i, int j, int k, int startX, int endX, int startY, int endY)
{
	uint8_t* AImage;
	uint8_t* BImage;

	static Pixel pixels[3];
	pixels[0].posX = i;
	pixels[0].posY = j;

	DividedImage image(startX, endX, startY, endY);

	int** rules = m_encryption.generateRules(m_scrambler.getValueKMR(i, j, k), m_scrambler.getValueRNS(i, j, k));
	int posX, posY;

	AImage = m_encryption.threadComputingLimitsOnScrambling(i, j, posX, posY, 0, rules, pixels[1], image);
	BImage = m_encryption.threadComputingLimitsOnScrambling(posX, posY, posX, posY, 1, rules, pixels[2], image);

	m_encryption.threadTransposingPixelColor(AImage, BImage, i, j, k, rules);
	setPixelsValue(pixels);

	for (int i = 0; i < 2; i++){
		delete[] rules[i];
	}
	delete[] rules;
	
	return pixels;
}

Pixel* ScramblingImage::threadDescramblingPixels(int i, int j, int k, int startX, int endX, int startY, int endY)
{
	uint8_t* AImage;
	uint8_t* BImage;

	static Pixel pixels[3];
	pixels[0].posX = i;
	pixels[0].posY = j;

	DividedImage image(startX, endX, startY, endY);

	int** rules = m_encryption.generateRules(m_scrambler.getValueKMR(i, j, k), m_scrambler.getValueRNS(i, j, k));
	int posX, posY;

	AImage = m_encryption.threadComputingLimitsOnScrambling(i, j, posX, posY, 0, rules, pixels[1], image);
	BImage = m_encryption.threadComputingLimitsOnScrambling(posX, posY, posX, posY, 1, rules, pixels[2], image);

	m_encryption.threadTransposingPixelColorDescrambling(AImage, BImage, i, j, k, rules);
	setPixelsValue(pixels);

	for (int i = 0; i < 2; i++) {
		delete[] rules[i];
	}
	delete[] rules;

	return pixels;
}

Pixel * ScramblingImage::getPixel(int posX, int posY)
{
	static Pixel* getPixel = new Pixel();
	uint8_t* value = m_picture.getRGBValueOfPixel(posX, posY);

	getPixel->posX = posX;
	getPixel->posY = posY;
	getPixel->R = value[0];
	getPixel->G = value[1];
	getPixel->B = value[2];

	return getPixel;
}

bool ScramblingImage::loadImage(std::string path)
{
	if (m_picture.loadImage(path))
	{
		m_scrambler.deallocateRules();
		m_scrambler.setNumberGenerateBits(m_picture.getRows(), m_picture.getColumns());

		return true;
	}

	return false;
}

void ScramblingImage::saveImage(std::string path)
{
	m_picture.saveImage(path);
}

void ScramblingImage::knightTravel(int x, int y, int moves, int chessBoard[][8])
{
	m_generatorKey.knightTravel(x, y, moves, chessBoard);
}

void ScramblingImage::generateKeys(double& key1, double& key2, int* chessBoard)
{
	m_generatorKey.generateKeys(key1, key2, chessBoard);
}

void ScramblingImage::generatePRBG(double* firstFt, double* secondFt)
{
	m_scrambler.generatePRBG(firstFt, secondFt);
}

void ScramblingImage::computeRulesKMR()
{
	m_scrambler.computeRulesKMR();
}

void ScramblingImage::computeRulesRNS()
{
	m_scrambler.computeRulesRNS();
}

double* ScramblingImage::computeChaoticMap(double key, bool controlParam)
{
	double controlParameter = controlParam ? m_scrambler.getFirstParamControl() : m_scrambler.getSecondParamControl();

	return m_scrambler.computeChaoticMap(key, controlParameter);
}

ScramblingImage::~ScramblingImage()
{

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
