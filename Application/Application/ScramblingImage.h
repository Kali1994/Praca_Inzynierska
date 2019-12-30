#pragma once
#include "stdafx.h"
/*
#include "PRBG.h"
#include "Picture.h"
#include "BitOperation.h"
#include "Extract.h"
#include "Encryption.h"
*/

#include "Picture.h"

class ScramblingImage
{
public:
	ScramblingImage(string,string);

	void vRunScrambling();
	void vRunDescrambling();

	int getTest();
	void setTest(int);

	void loadImage(std::string path);
	void saveImage(std::string path);

	~ScramblingImage();
private:
	//PRBG* m_piPRGB;
	//Encryption* m_piENC;
	Picture m_picture;

	int m_test;
};