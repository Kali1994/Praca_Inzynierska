// stdafx.cpp : source file that includes just the standard includes
// Application.pch will be the pre-compiled header
// stdafx.obj will contain the pre-compiled type information

#pragma once
#include "stdafx.h"

// TODO: reference any additional headers you need in STDAFX.H
// and not in this file

uint8_t*** allocateMemory(uint8_t*** matrix, int rows, int columns)
{
	matrix = new uint8_t**[rows];
	for (int i = 0; i < rows; i++)
	{
		matrix[i] = new uint8_t*[columns];
		for (int j = 0; j < columns; j++)
		{
			matrix[i][j] = new uint8_t[3];
		}
	}

	return matrix;
}

void deallocateMemory(uint8_t*** matrix, int rows, int columns)
{
	for (int i = 0; i < rows; i++)
	{
		for (int j = 0; j < columns; j++)
		{
			delete[] matrix[i][j];
		}

		delete[] matrix[i];
	}

	delete[] matrix;
}
