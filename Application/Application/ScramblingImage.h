#pragma once
#include "stdafx.h"
/*
#include "PRBG.h"
#include "Picture.h"
#include "BitOperation.h"
#include "Extract.h"
#include "Encryption.h"
*/

#include "PRBG.h"
#include "Picture.h"
#include "KnightTourGenerator.h"
#include "Encryption.h"

class ScramblingImage
{
public:
	ScramblingImage();

	Pixel* scramblingPixels(int, int, int);
	Pixel* descramblingPixels(int, int, int);

	Pixel* threadScramblingPixels(int, int, int, int, int, int, int);
	Pixel* threadDescramblingPixels(int, int, int, int, int, int, int);

	bool loadImage(std::string path);
	void saveImage(std::string path);

	void knightTravel(int x, int y, int moves, int chessBoard[][8]);
	void generateKeys(double&, double&, int* chessBoard);
	void generatePRBG(double*, double*);

	void computeRulesKMR();
	void computeRulesRNS();

	double* computeChaoticMap(double, bool);

	~ScramblingImage();
private:
	void setPixelsValue(Pixel*);
	
	Picture m_picture;
	Encryption m_encryption;
	KnightTourGenerator m_generatorKey;
	PRBG m_scrambler;
};