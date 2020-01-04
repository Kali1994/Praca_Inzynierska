#pragma once
#include "stdafx.h"
#include "Picture.h"

Picture::Picture() :
	m_rows(0),
	m_columns(0),
	m_matrix(nullptr)
{

}

Picture::~Picture()
{
	if (m_matrix != nullptr)
	{
		deallocateMemory(m_matrix, m_rows, m_columns);
		m_matrix = nullptr;
	}
}

void Picture::loadImage(std::string path)
{
	if (m_matrix != nullptr)
	{
		deallocateMemory(m_matrix, m_rows, m_columns);
		m_matrix = nullptr;
	}

	m_image = imread(path);

	m_rows = m_image.rows;
	m_columns = m_image.cols;

	m_matrix = allocateMemory(m_matrix, m_rows, m_columns);
	pictureLoadValue();
}

void Picture::saveImage(std::string path)
{
	writeToImage();
	imwrite(path, m_image);
}

void Picture::setValueofPixel(int i, int j, int k, uint8_t u8Value)
{
	m_matrix[i][j][k] = u8Value;
}

void Picture::setPixel(int i, int j, uint8_t* RGB)
{
	for (int k = 0; k < 3; k++)
	{
		m_matrix[i][j][k] = RGB[k];
	}
}

uint8_t* Picture::getRGBValueOfPixel(int i, int j)
{
	return m_matrix[i][j];
}

uint8_t Picture::getValueOfPixel(int i, int j, int k)
{
	return m_matrix[i][j][k];
}

int Picture::getRows(void)
{
	return m_rows;
}

int Picture::getColumns(void)
{
	return m_columns;
}

void Picture::pictureLoadValue()
{
	for (int i = 0; i < m_rows; i++)
	{
		for (int j = 0; j < m_columns; j++)
		{
			m_matrix[i][j][0] = m_image.at<cv::Vec3b>(i, j)[2];
			m_matrix[i][j][1] = m_image.at<cv::Vec3b>(i, j)[1];
			m_matrix[i][j][2] = m_image.at<cv::Vec3b>(i, j)[0];
		}
	}
}

void Picture::writeToImage()
{
	for (int i = 0; i < m_rows; i++)
	{
		for (int j = 0; j < m_columns; j++)
		{
			m_image.at<cv::Vec3b>(i, j)[2] = m_matrix[i][j][0];
			m_image.at<cv::Vec3b>(i, j)[1] = m_matrix[i][j][1];
			m_image.at<cv::Vec3b>(i, j)[0] = m_matrix[i][j][2];
		}
	}
}
