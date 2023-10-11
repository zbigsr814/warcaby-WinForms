using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warcamy2
{
	public class RuchyPionkow
	{
		static public int Width = 0;
		static public int Height = 0;
		static public bool ruchPionkaNaPuste(Pole poleZazn, Pole pionekDoRuchu)
		{
			Pole tmpPole = (Pole)poleZazn.Clone();

			poleZazn.wspX = pionekDoRuchu.wspX;
			poleZazn.wspY = pionekDoRuchu.wspY;
			poleZazn.Location = new Point(pionekDoRuchu.wspX * (Width + 3), pionekDoRuchu.wspY * (Width + 3));
			pionekDoRuchu.wspX = tmpPole.wspX;
			pionekDoRuchu.wspY = tmpPole.wspY;
			pionekDoRuchu.Location = new Point(tmpPole.wspX * (Width + 3), tmpPole.wspY * (Width + 3));	// przemieszczenie

			int ix = (poleZazn.wspX - pionekDoRuchu.wspX) / Math.Abs(poleZazn.wspX - pionekDoRuchu.wspX);   // zbijanie
			int iy = (poleZazn.wspY - pionekDoRuchu.wspY) / Math.Abs(poleZazn.wspX - pionekDoRuchu.wspX);
			foreach (var iterPole in Szachy.pola)
			{
				if ((iterPole.wspX == (pionekDoRuchu.wspX + ix)) && (iterPole.wspY == (pionekDoRuchu.wspY + iy)))
				{
					if (pionekDoRuchu.rodzaj == (int)typPola.czarnyPionek	// zbijanie przez czarne pionki
						&& (iterPole.rodzaj == (int)typPola.bialyPionek || iterPole.rodzaj == (int)typPola.bialaKrolowa))
					{
						iterPole.rodzaj = (int)typPola.puste;
						iterPole.Image = null;
						return true;
					} 
					else if (pionekDoRuchu.rodzaj == (int)typPola.bialyPionek   // zbijanie przez białe pionki
						&& (iterPole.rodzaj == (int)typPola.czarnyPionek || iterPole.rodzaj == (int)typPola.czarnaKrolowa))
					{
						iterPole.rodzaj = (int)typPola.puste;
						iterPole.Image = null;
						return true;
					}
				}

			}
			return false;
		}

		static public bool ruchKrolowyNaPuste(Pole poleZazn, Pole pionekDoRuchu)
		{
			bool kontynuujRuch = false;

			Pole tmpPole = (Pole)poleZazn.Clone();

			poleZazn.wspX = pionekDoRuchu.wspX;
			poleZazn.wspY = pionekDoRuchu.wspY;
			poleZazn.Location = new Point(pionekDoRuchu.wspX * (Width + 3), pionekDoRuchu.wspY * (Width + 3));
			pionekDoRuchu.wspX = tmpPole.wspX;
			pionekDoRuchu.wspY = tmpPole.wspY;
			pionekDoRuchu.Location = new Point(tmpPole.wspX * (Width + 3), tmpPole.wspY * (Width + 3));

			if (Math.Abs(poleZazn.wspX - pionekDoRuchu.wspX) == Math.Abs(poleZazn.wspY - pionekDoRuchu.wspY)) //return true;
			{
				for (int i = 1; i < Math.Abs(poleZazn.wspX - pionekDoRuchu.wspX); i++)
				{
					int ix = (poleZazn.wspX - pionekDoRuchu.wspX) / Math.Abs(poleZazn.wspX - pionekDoRuchu.wspX);
					int iy = (poleZazn.wspY - pionekDoRuchu.wspY) / Math.Abs(poleZazn.wspX - pionekDoRuchu.wspX);
					foreach (var iterPole in Szachy.pola)
					{
						if (pionekDoRuchu.rodzaj == (int)typPola.bialaKrolowa)		// dla bialej krolowej
						{
							if ((iterPole.wspX == (pionekDoRuchu.wspX + ix * i)) && (iterPole.wspY == (pionekDoRuchu.wspY + iy * i)))
							{
								if (iterPole.rodzaj == (int)typPola.czarnaKrolowa || iterPole.rodzaj == (int)typPola.czarnyPionek)
								{
									iterPole.Image = null;
									iterPole.rodzaj = (int)typPola.puste;
									kontynuujRuch = true;
								}
							}
						}
						else if (pionekDoRuchu.rodzaj == (int)typPola.czarnaKrolowa)    // dla czarnej krolowej
						{
							if ((iterPole.wspX == (pionekDoRuchu.wspX + ix * i)) && (iterPole.wspY == (pionekDoRuchu.wspY + iy * i)))
							{
								if (iterPole.rodzaj == (int)typPola.bialaKrolowa || iterPole.rodzaj == (int)typPola.bialyPionek)
								{
									iterPole.Image = null;
									iterPole.rodzaj = (int)typPola.puste;
									kontynuujRuch = true;
								}
							}
						}

					}

				}
				Console.WriteLine();
			}
			return kontynuujRuch;
		}

		static public void SprawdzZamienNaKrolawa(Pole pionekDoRuchu)
		{
			if (pionekDoRuchu.rodzaj == (int)typPola.czarnyPionek && pionekDoRuchu.wspY == 7)
			{
				pionekDoRuchu.rodzaj = (int)typPola.czarnaKrolowa;
				pionekDoRuchu.Image = null;
				pionekDoRuchu.Image = Pole.obrCzarnaKrolowa;
			}
			else if (pionekDoRuchu.rodzaj == (int)typPola.bialyPionek && pionekDoRuchu.wspY == 0)
			{
				pionekDoRuchu.rodzaj = (int)typPola.bialaKrolowa;
				pionekDoRuchu.Image = Pole.obrBialaKrolowa;
			}
		}
	}
}
