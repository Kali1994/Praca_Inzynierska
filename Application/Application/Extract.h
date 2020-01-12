#pragma once
#include "stdafx.h"
#include "PRBG.h"

class Extract: public BitOperation
{
public:
	Extract();

	void generateKnightRules(uint8_t, uint8_t);
	int **generateRules(uint8_t, uint8_t);

	~Extract();
protected:
	int m_iArrRules[2][3];
};