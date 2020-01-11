#pragma once
#include "stdafx.h"
#include "KnightTourGenerator.h"

KnightTourGenerator::KnightTourGenerator()
{

}

KnightTourGenerator::~KnightTourGenerator()
{

}

void KnightTourGenerator::generateKeys(double& key1, double& key2, int* chessBoard)
{
	srand(time(NULL));

	std::string firstKey;
	std::string secondKey;

	firstKey = ((rand() % 2) == 0) ? "-0." : "0.";
	secondKey = ((rand() % 2) == 0) ? "-0." : "0.";

	std::string path;

	for (int i = 0; i < 64; i++)
	{
		path += BitOperation::sDec2Bin(chessBoard[i]);
	}

	int nr = rand() % 400;

	std::string keyOne = path.substr(nr, 63);
	std::string keyTwo = path.substr(nr + 63, 63);

	firstKey += std::to_string(stoll(keyOne.c_str(), nullptr, 2)).substr(0, 12);
	secondKey += std::to_string(stoll(keyTwo.c_str(), nullptr, 2)).substr(0, 12);

	key1 = stod(firstKey.c_str());
	key2 = stod(secondKey.c_str());
}

bool KnightTourGenerator::knightTravel(int x, int y, int moves, int chessBoard[][8])
{
	int move = 0;

	if (moves == 64)
	{
		return true;
	}

	if (chessBoard[x][y] == 0) {
		while (move < 8) {
			if (moveX(x, move) != -1 && moveY(y, move) != -1) {
				chessBoard[x][y] = moves;
				if (knightTravel(moveX(x, move), moveY(y, move), moves + 1, chessBoard)) {
					return true;
				}
			}
			move++;
		}
		chessBoard[x][y] = 0;
	}

	return false;
}

int KnightTourGenerator::moveX(int x, int move)
{
	if (move == 0) {
		x = x + 1;
	}
	else if (move == 1) {
		x = x + 2;
	}
	else if (move == 2) {
		x = x + 2;
	}
	else if (move == 3) {
		x = x + 1;
	}
	else if (move == 4) {
		x = x - 1;
	}
	else if (move == 5) {
		x = x - 2;
	}
	else if (move == 6) {
		x = x - 2;
	}
	else if (move == 7) {
		x = x - 1;
	}

	if (x < 0 || x > 7) {
		return -1;
	}
	else
	{
		return x;
	}
}

int KnightTourGenerator::moveY(int y, int move)
{
	if (move == 0) {
		y = y - 2;
	}
	else if (move == 1) {
		y = y - 1;
	}
	else if (move == 2) {
		y = y + 1;
	}
	else if (move == 3) {
		y = y + 2;
	}
	else if (move == 4) {
		y = y + 2;
	}
	else if (move == 5) {
		y = y + 1;
	}
	else if (move == 6) {
		y = y - 1;
	}
	else if (move == 7) {
		y = y - 2;
	}

	if (y < 0 || y > 7) {
		return -1;
	}
	else
	{
		return y;
	}
}
