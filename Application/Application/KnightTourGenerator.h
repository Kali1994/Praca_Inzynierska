#pragma once
#include "stdafx.h"
#include "BitOperation.h"

class KnightTourGenerator : public BitOperation
{
public:
	KnightTourGenerator();
	~KnightTourGenerator();

	void generateKeys(double& key1, double& key2, int* chessBoard);
	bool knightTravel(int x, int y, int moves, int chessBoard[][8]);

private:
	int moveX(int x, int move);
	int moveY(int y, int move);
};