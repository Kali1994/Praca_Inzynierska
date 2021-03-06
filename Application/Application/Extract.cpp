#pragma once
#include "stdafx.h"
#include "Extract.h"

Extract::Extract()
{

}

Extract::~Extract()
{

}

void Extract::generateKnightRules(uint8_t u8KMR, uint8_t u8RNS)
{
	string sRule = BitOperation::sDec2Bin(u8KMR);

	m_iArrRules[0][0] = BitOperation::iBin2Dec(sRule,1,3);
	m_iArrRules[1][0] = BitOperation::iBin2Dec(sRule,5,3);

	m_iArrRules[0][2] = BitOperation::bCheckBit(sRule, 4);
	m_iArrRules[1][2] = BitOperation::bCheckBit(sRule, 8);

	m_iArrRules[0][1] = u8RNS;
	string sNb = BitOperation::sDec2Bin(m_iArrRules[0][1]);
	sNb = BitOperation::sFliplr(sNb);
	m_iArrRules[1][1] = iBin2Dec(sNb, 1, BitOperation::m_numberBits);

}

int** Extract::generateRules(uint8_t u8KMR, uint8_t u8RNS)
{
	int ** rules = new int*[2];
	for (int i = 0; i < 2; i++) {
		rules[i] = new int[3];
	}

	string sRule = BitOperation::sDec2Bin(u8KMR);

	rules[0][0] = BitOperation::iBin2Dec(sRule, 1, 3);
	rules[1][0] = BitOperation::iBin2Dec(sRule, 5, 3);

	rules[0][2] = BitOperation::bCheckBit(sRule, 4);
	rules[1][2] = BitOperation::bCheckBit(sRule, 8);

	rules[0][1] = u8RNS;
	string sNb = BitOperation::sDec2Bin(rules[0][1]);
	sNb = BitOperation::sFliplr(sNb);
	rules[1][1] = iBin2Dec(sNb, 1, BitOperation::m_numberBits);

	return rules;
}
