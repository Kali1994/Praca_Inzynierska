#pragma once
#include "stdafx.h"
#include "Picture.h"
#include "Extract.h"

class Encryption : public Extract
{
public:
	Encryption(Picture*);

	uint8_t* computingLimitsOnScrambling(int, int, int, Pixel&);

	void transposingPixelColor(uint8_t*, uint8_t*, int, int, int);
	void transposingPixelColorDescrambling(uint8_t*, uint8_t*, int, int, int);
	
	~Encryption();
private:
	int m_Oldi;
	int m_Oldj;
protected:
	Picture* m_picture;
};
