#pragma once
#include "stdafx.h"

class Picture
{
public:
	Picture(string,string);

	void vPictureLoadValue(void);
	void vDisplayImage(void);
	void vWriteToImage(void);
	void vSaveImage(void);
	void vSetValueofPixel(int, int, int, uint8_t);
	void vSetPixel(int, int, uint8_t*);
	
	uint8_t* u8GetRGBValueOfPixel(int,int);
	uint8_t u8GetValueOfPixel(int, int, int);

	int iGetSize(void);

	~Picture();

private:
	string m_sName;
	string m_sResult;
	string m_sWindowName;

	uint8_t*** m_piMatrix;
	Mat m_Image;
	int m_iSize;

};
