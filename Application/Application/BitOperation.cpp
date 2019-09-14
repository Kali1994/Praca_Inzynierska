#include "stdafx.h"
#include "BitOperation.h"

int BitOperation::iBin2Dec(string sBinaryValue, int iStart, int iEnd)
{
	int  iReturnValue = 0;
	size_t iPower;

	sBinaryValue = sBinaryValue.substr(iStart-1, iEnd);

	while (sBinaryValue.length() < m_iNumberBits)
	{
		sBinaryValue = "0" + sBinaryValue;
	}

	iPower = sBinaryValue.length() - 1;

	for (int i = 0; i < sBinaryValue.length(); ++i)
	{
		iReturnValue +=(int)(sBinaryValue[i] == '1' ? pow(2, iPower) : 0);
		iPower--;
	}

	return iReturnValue;
}

string BitOperation::sDec2Bin(int iDecValue)
{
	string sReturnValue;

	for (int i = m_iNumberBits; i > 0; --i)
	{
		sReturnValue.push_back(cReadBit(iDecValue, i));
	}
	return sReturnValue;
}

char BitOperation::cReadBit(int iDecValue, int iPosition)
{
	char cReturnValue;
	
	(iDecValue & (1 << (iPosition-1))) ? cReturnValue = '1' : cReturnValue = '0';

	return cReturnValue;
}

bool BitOperation::bCheckBit(string sBinaryValue,int iPosition)
{
	bool bReturnValue = true;
	if (sBinaryValue[iPosition - 1] == '0')
	{
		bReturnValue = false;
	}

	return bReturnValue;
}

string BitOperation::sFliplr(string sBinaryValue)
{
	string sReturnValue;

	for (int i = 0; i < sBinaryValue.length(); ++i)
	{
		sReturnValue.push_back(sBinaryValue[sBinaryValue.length() - i - 1]);
	}

	return sReturnValue;

}



