#pragma once
#include "stdafx.h"
#include "PRBG.h"
#include "Picture.h"
#include "BitOperation.h"
#include "Extract.h"
#include "Encryption.h"

class __declspec(dllexport) ScramblingImage
{
public:
	ScramblingImage(string,string);

	void vRunScrambling();
	void vRunDescrambling();

	~ScramblingImage();
private:
	PRBG* m_piPRGB;
	Encryption* m_piENC;
	Picture* m_piPicture;
};