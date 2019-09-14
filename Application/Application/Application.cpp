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
		cout << "Wprowadz nazwe pliku do szyfrowania" << endl;
		cin >> Firstname;

		cout << "Wprowadz nazwe pliku wyjsciowego" << endl;
		cin >> SecondName;

		Firstname += ".png";
		SecondName += ".png";

		ScramblingImage *piApp = new ScramblingImage(Firstname, SecondName);
		piApp->vRunScrambling();

		delete piApp;
	}
	else if (2 == Chose)
	{
		cout << "Wprowadz nazwe pliku do Deszyfrowania" << endl;
		cin >> Firstname;

		cout << "Wprowadz nazwe pliku wyjsciowego" << endl;
		cin >> SecondName;

		Firstname += ".png";
		SecondName += ".png";

		ScramblingImage *piDesc = new ScramblingImage(Firstname, SecondName);
		piDesc->vRunDescrambling();



		delete piDesc;

	}
	else
	{
		cout << "Nieznana opcja" << endl;
	}

	system("pause");
	return 0;
}

