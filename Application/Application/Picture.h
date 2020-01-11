#pragma once
#include "stdafx.h"

class Picture
{
public:
	Picture();
	~Picture();

	bool loadImage(std::string path);
	void saveImage(std::string path);

	void setValueofPixel(int, int, int, uint8_t);
	void setPixel(int, int, uint8_t*);
	
	uint8_t* getRGBValueOfPixel(int, int);
	uint8_t getValueOfPixel(int, int, int);

	int getRows(void);
	int getColumns(void);

private:
	void pictureLoadValue(void);
	void writeToImage(void);

	int m_rows;
	int m_columns;

	uint8_t*** m_matrix;

	Mat m_image;
};
