#pragma once
#include "stdafx.h"

class BitOperation
{
public:
	BitOperation(){};

	int iBin2Dec(string,int,int);
	bool bCheckBit(string, int);
	char cReadBit(int, int);

	string sDec2Bin(int);
	string sFliplr(string);

protected:
	const int m_numberBits = 8;

};
