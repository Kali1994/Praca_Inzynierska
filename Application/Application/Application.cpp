// Application.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "ScramblingImage.h"

int main()
{
	int Chose;
	string Firstname;
	string SecondName;

	cout << "1 Szyfrowanie" << endl;
	cout << "2 Deszyfracja" << endl;

	cin >> Chose;

	if (1 == Chose)
	{
		ScramblingImage *app = new ScramblingImage();
		app->loadImage("1.png");
		app->preparingRules(0.687754925117, -0.013462335467);


		for (int i = 0; i < 512; i++)
		{
			for (int j = 0; j < 512; j++)
			{
				for (int k = 0; k < 3; k++)
				{
					app->scramblingPixels(i, j, k);
				}
			}
		}

//		delete piApp;
	}
	else if (2 == Chose)
	{
		cout << "Wprowadz nazwe pliku do Deszyfrowania" << endl;
		cin >> Firstname;

		cout << "Wprowadz nazwe pliku wyjsciowego" << endl;
		cin >> SecondName;

		Firstname += ".png";
		SecondName += ".png";

	//	ScramblingImage *piDesc = new ScramblingImage(Firstname, SecondName);
	//	piDesc->vRunDescrambling();



	//	delete piDesc;

	}
	else
	{
		cout << "Nieznana opcja" << endl;
	}

	system("pause");
	return 0;
}

