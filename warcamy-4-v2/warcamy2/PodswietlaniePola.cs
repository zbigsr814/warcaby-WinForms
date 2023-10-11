using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warcamy2
{
	internal class PodswietlaniePola
	{
		static public int Width = 0;
		static public int Height = 0;
		// pole.ustawKolor(Color.Blue);

		static public void czyszczeniePol()
		{

		}
		static public void ruchPionkaNaPuste(Pole pionekDoRuchu, bool tylkoDoPrzodu)
		{
			pionekDoRuchu.ustawKolor(Color.OrangeRed);

			#region Przemieszczanie pionków o jeden - podświetlenie
			for (int i = -1; i < 2; i += 2)
			{
				int j = -1;
				int jEnd = 2;

				if (tylkoDoPrzodu && pionekDoRuchu.rodzaj == (int)typPola.czarnyPionek) { j = 1; jEnd = 2; }	// zaznaczenie tylko do przodu
				if (tylkoDoPrzodu && pionekDoRuchu.rodzaj == (int)typPola.bialyPionek) { j = -1; jEnd = 0; }	// dla pionków

				for (; j < jEnd; j += 2)
				{
					foreach (var iterPole in Szachy.pola)
					{
						if ((iterPole.wspX == (pionekDoRuchu.wspX + i)) && (iterPole.wspY == (pionekDoRuchu.wspY + j)))
						{
							if (iterPole.rodzaj == (int)typPola.puste) iterPole.ustawKolor(Color.Green);

						}

					}
				}
			}
			#endregion
			#region Zbijanie przez pionki - podświetlenie
			Pole wykPuste = null, wykPionek = null;
			if (pionekDoRuchu.rodzaj == (int)typPola.czarnyPionek)
			{
				for (int i = -1; i < 2; i += 2)		// +1 / +2 do X
				{
					for (int j = -1; j < 2; j += 2)	// +1 / +2 do Y
					{
						foreach (var iterPole in Szachy.pola)	// przeszukanie szachownicy
						{
							if (iterPole.wspX == pionekDoRuchu.wspX + i && iterPole.wspY == pionekDoRuchu.wspY + j		// zapisz pole z pionkiem
								&& (iterPole.rodzaj == (int)typPola.bialyPionek || iterPole.rodzaj == (int)typPola.bialaKrolowa)) wykPionek = iterPole;

							if (iterPole.wspX == pionekDoRuchu.wspX + 2*i && iterPole.wspY == pionekDoRuchu.wspY + 2*j	// zapisz puste pole
								&& iterPole.rodzaj == (int)typPola.puste) wykPuste = iterPole;
						}

						if (wykPionek != null && wykPuste != null)	// jeśli wystąpi +1 pionek / +2 puste to podświetl
						{
							wykPionek.ustawKolor(ParColor.doZbicia);
							wykPuste.ustawKolor(ParColor.naPuste);
						}

						wykPionek = null;	// wyzeruj zaznaczone pionki
						wykPuste = null;
					}
				}
			}
			else if (pionekDoRuchu.rodzaj == (int)typPola.bialyPionek)
			{
				for (int i = -1; i < 2; i += 2)     // +1 / +2 do X
				{
					for (int j = -1; j < 2; j += 2) // +1 / +2 do Y
					{
						foreach (var iterPole in Szachy.pola)   // przeszukanie szachownicy
						{
							if (iterPole.wspX == pionekDoRuchu.wspX + i && iterPole.wspY == pionekDoRuchu.wspY + j      // zapisz pole z pionkiem
								&& (iterPole.rodzaj == (int)typPola.czarnyPionek || iterPole.rodzaj == (int)typPola.czarnaKrolowa)) wykPionek = iterPole;

							if (iterPole.wspX == pionekDoRuchu.wspX + 2 * i && iterPole.wspY == pionekDoRuchu.wspY + 2 * j  // zapisz puste pole
								&& iterPole.rodzaj == (int)typPola.puste) wykPuste = iterPole;
						}

						if (wykPionek != null && wykPuste != null)  // jeśli wystąpi +1 pionek / +2 puste to podświetl
						{
							wykPionek.ustawKolor(ParColor.doZbicia);
							wykPuste.ustawKolor(ParColor.naPuste);
						}

						wykPionek = null;   // wyzeruj zaznaczone pionki
						wykPuste = null;
					}
				}
			}
			#endregion
			#region Zbijanie przez Królowe - podświetlenie
			if (pionekDoRuchu.rodzaj == (int)typPola.czarnaKrolowa || pionekDoRuchu.rodzaj == (int)typPola.bialaKrolowa)
			{
				// tworzenie list do informacji o pionkach na 4 przekątnych
				List<Pole> zbitePionki1 = new List<Pole>();
				List<Pole> zbitePionki2 = new List<Pole>();
				List<Pole> zbitePionki3 = new List<Pole>();
				List<Pole> zbitePionki4 = new List<Pole>();

				// przeszukiwanie listy pionków
				for (int i = 1; i < 10; i++)
				{
					foreach (var iterPole in Szachy.pola)
					{
						if (iterPole.wspX == pionekDoRuchu.wspX + i && iterPole.wspY == pionekDoRuchu.wspY + i) zbitePionki1.Add(iterPole);
						if (iterPole.wspX == pionekDoRuchu.wspX + i && iterPole.wspY == pionekDoRuchu.wspY - i) zbitePionki2.Add(iterPole);
						if (iterPole.wspX == pionekDoRuchu.wspX - i && iterPole.wspY == pionekDoRuchu.wspY + i) zbitePionki3.Add(iterPole);
						if (iterPole.wspX == pionekDoRuchu.wspX - i && iterPole.wspY == pionekDoRuchu.wspY - i) zbitePionki4.Add(iterPole);
					}
				}

				foreach (var numer in zbitePionki1)
				{
					Console.Write(numer.rodzaj + " ");
				}
				Console.WriteLine();

				foreach (var numer in zbitePionki2)
				{
					Console.Write(numer.rodzaj + " ");
				}
				Console.WriteLine();

				foreach (var numer in zbitePionki3)
				{
					Console.Write(numer.rodzaj + " ");
				}
				Console.WriteLine();

				foreach (var numer in zbitePionki4)
				{
					Console.Write(numer.rodzaj + " ");
				}
				Console.WriteLine();

				algorytmKrolowej(new List<List<Pole>> { zbitePionki1, zbitePionki2, zbitePionki3, zbitePionki4 }, pionekDoRuchu);
			}
			#endregion

		}

		private static void algorytmKrolowej(List<List<Pole>> metaPionki, Pole pionekDoRuchu)
		{
			int pionkiObokSiebie = 0;
			foreach (var zbior in metaPionki)
			{
				for (int i = 0; i < zbior.Count; i++)
				{
					if (zbior[i].rodzaj == (int)typPola.puste) zbior[i].ustawKolor(ParColor.naPuste);

					if (pionekDoRuchu.rodzaj == (int)typPola.czarnaKrolowa)      // czarne krolowe nie zbijaja czarnych pionkow/krolow
					{	
						try
						{
							if (zbior[i].rodzaj == (int)typPola.czarnaKrolowa || zbior[i].rodzaj == (int)typPola.czarnyPionek) break;
							else if ((zbior[i].rodzaj == (int)typPola.bialaKrolowa || zbior[i].rodzaj == (int)typPola.bialyPionek)
								&& zbior[i + 1].rodzaj != (int)typPola.puste) break;
							else if (zbior[i].rodzaj != (int)typPola.puste) zbior[i].ustawKolor(ParColor.doZbicia);
						}
						catch (ArgumentOutOfRangeException)
						{
							break;
						}
						
					}

					if (pionekDoRuchu.rodzaj == (int)typPola.bialaKrolowa)      // biale krolowe nie zbijaja bialych pionkow/krolow
					{
						try
						{
							if (zbior[i].rodzaj == (int)typPola.bialaKrolowa || zbior[i].rodzaj == (int)typPola.bialyPionek) break;
							else if ((zbior[i].rodzaj == (int)typPola.czarnaKrolowa || zbior[i].rodzaj == (int)typPola.czarnyPionek)
								&& zbior[i + 1].rodzaj != (int)typPola.puste) break;
							else if (zbior[i].rodzaj != (int)typPola.puste) zbior[i].ustawKolor(ParColor.doZbicia);
						}
						catch (ArgumentOutOfRangeException)
						{
							break;
						}
						}
						
				}
			}
		}

		static public void resetujKolorySzachownicy()
		{
			foreach (var iterPole in Szachy.pola)
			{
				iterPole.resetKolor();
			}
		}
	}
}
