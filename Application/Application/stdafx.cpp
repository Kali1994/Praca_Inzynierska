// stdafx.cpp : source file that includes just the standard includes
// Application.pch will be the pre-compiled header
// stdafx.obj will contain the pre-compiled type information

#include "stdafx.h"

// TODO: reference any additional headers you need in STDAFX.H
// and not in this file
uint8_t*** piu8AllocateMemory(uint8_t*** piu8Matrix,int iSize)
{
	piu8Matrix = new uint8_t**[iSize];
	for (int i = 0; i<iSize; i++)
	{
		piu8Matrix[i] = new uint8_t*[iSize];
		for (int j = 0; j<iSize; j++)
		{
			piu8Matrix[i][j] = new uint8_t[3];
		}
	}

	return piu8Matrix;
}