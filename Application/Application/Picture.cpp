#pragma once
#include "stdafx.h"
#include "Picture.h"


Picture::Picture() 
{
	/*
	this->m_Image = imread(sName);
	if (m_Image.empty())
	{
		cout << "Could not open or find the image" << endl;
	}
	else
	{
		this->m_sWindowName = "Obraz";
		this->m_iSize = m_Image.rows;

		m_piMatrix = piu8AllocateMemory(m_piMatrix, m_iSize);
		this->vPictureLoadValue();
	}
	*/
	
}

//void Picture::vPictureLoadValue()
//{
//	for (int i = 0; i <m_Image.rows; i++)
//	{
//		for (int j = 0; j < m_Image.cols; j++)
//		{
//			m_piMatrix[i][j][0] = m_Image.at<cv::Vec3b>(i, j)[2];
//			m_piMatrix[i][j][1] = m_Image.at<cv::Vec3b>(i, j)[1];
//			m_piMatrix[i][j][2] = m_Image.at<cv::Vec3b>(i, j)[0];
//		}
//	}
//
//}
/*
uint8_t* Picture::u8GetRGBValueOfPixel(int i, int j)
{
	return m_piMatrix[i][j];
}

void Picture::vDisplayImage()
{
	if (m_Image.empty())
	{
		cout << "Could not open or find the image" << endl;
	}
	else
	{
		m_sWindowName = "Picture";
		namedWindow(m_sWindowName);
		imshow(m_sWindowName, m_Image);
		waitKey(0); 
	}
}

int Picture::iGetSize()
{
	return m_iSize;
}

Picture::~Picture()
{
	if (!m_Image.empty())
	{
		destroyWindow(m_sWindowName);
	}
}

void Picture::vSetValueofPixel(int i, int j, int k,uint8_t u8Value)
{
	m_piMatrix[i][j][k] = u8Value;
}

void Picture::vSetPixel(int i, int j, uint8_t* RGB)
{
	for (int k = 0; k < 3; k++)
	{
		m_piMatrix[i][j][k] = RGB[k];
	}
}
*/
//void Picture::vWriteToImage()
//{
//	for (int i = 0; i < m_iSize; i++)
//	{
//		for (int j = 0; j < m_iSize; j++)
//		{
//			m_Image.at<cv::Vec3b>(i, j)[2] = m_piMatrix[i][j][0];
//			m_Image.at<cv::Vec3b>(i, j)[1] = m_piMatrix[i][j][1];
//			m_Image.at<cv::Vec3b>(i, j)[0] = m_piMatrix[i][j][2];
//		}
//	}
//}

void Picture::loadImage(std::string path)
{
	this->m_Image = imread(path);
	//this->m_iSize = m_Image.rows;

	//m_piMatrix = piu8AllocateMemory(m_piMatrix, m_iSize);
	//this->vPictureLoadValue();
}

void Picture::saveImage(std::string path)
{
	//vWriteToImage();
	imwrite(path, m_Image);
}

/*
uint8_t Picture::u8GetValueOfPixel(int i, int j, int k)
{
	return m_piMatrix[i][j][k];
}

*/
