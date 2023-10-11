using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using warcamy2;

namespace warcamy2
{
	public static class WarunkiPionkow
	{
		

		public static bool pionekNaPuste(Pole poleZazn, Pole pionekDoRuchu)
		{
			// pionki nie mogą się cofać
			if(pionekDoRuchu.rodzaj == (int)typPola.czarnyPionek)
			{
				if (Math.Abs(poleZazn.wspX - pionekDoRuchu.wspX) == 1 && (poleZazn.wspY - pionekDoRuchu.wspY == 1)) return true;    // ruch o 1 pole
			}
			if (pionekDoRuchu.rodzaj == (int)typPola.bialyPionek)
			{
				if (Math.Abs(poleZazn.wspX - pionekDoRuchu.wspX) == 1 && (pionekDoRuchu.wspY - poleZazn.wspY == 1)) return true;    // ruch o 1 pole
			}

			// zbijanie przez pionka innego pionka / krolowe
			int ix = (poleZazn.wspX - pionekDoRuchu.wspX) / Math.Abs(poleZazn.wspX - pionekDoRuchu.wspX);	
			int iy = (poleZazn.wspY - pionekDoRuchu.wspY) / Math.Abs(poleZazn.wspX - pionekDoRuchu.wspX);
			foreach (var iterPole in Szachy.pola)
			{
				if ((iterPole.wspX == (pionekDoRuchu.wspX + ix)) && (iterPole.wspY == (pionekDoRuchu.wspY + iy)))
				{
					if (pionekDoRuchu.rodzaj == (int)typPola.czarnyPionek   // zbijanie przez czarnego pionka bialego
						&& (iterPole.rodzaj == (int)typPola.bialyPionek || iterPole.rodzaj == (int)typPola.bialaKrolowa)) return true;
					else if (pionekDoRuchu.rodzaj == (int)typPola.bialyPionek   // zbijanie przez bialego pionka czarnego
						&& (iterPole.rodzaj == (int)typPola.czarnyPionek || iterPole.rodzaj == (int)typPola.czarnaKrolowa)) return true;
				}
				
			}
			return false;
		}

		public static bool krolowaNaPuste(Pole poleZazn, Pole pionekDoRuchu)
		{
			List<int> zbitePionki = new List<int>();
			if (Math.Abs(poleZazn.wspX - pionekDoRuchu.wspX) == Math.Abs(poleZazn.wspY - pionekDoRuchu.wspY)) //return true;
			{
				for (int i = 1; i < Math.Abs(poleZazn.wspX - pionekDoRuchu.wspX); i++)
				{
					int ix = (poleZazn.wspX - pionekDoRuchu.wspX) / Math.Abs(poleZazn.wspX - pionekDoRuchu.wspX);
					int iy = (poleZazn.wspY - pionekDoRuchu.wspY) / Math.Abs(poleZazn.wspX - pionekDoRuchu.wspX);
					foreach (var iterPole in Szachy.pola)
					{
						
						if ((iterPole.wspX == (pionekDoRuchu.wspX + ix * i)) && (iterPole.wspY == (pionekDoRuchu.wspY + iy * i)))
						{
                            Console.Write(iterPole.rodzaj.ToString() + " ");
							zbitePionki.Add(iterPole.rodzaj);	// zapamietanie jakie pionki znajdują sie na drodze
                        }
					}
				}
                Console.WriteLine();

				return czyMoznaZbic(zbitePionki, pionekDoRuchu);	// analiza zapamietanej drogi
			}
			else return false;
		}

		private static bool czyMoznaZbic(List<int> zbitePionki, Pole pionekDoRuchu)		// zabezpieczenia aby nie mozna bylo usuwac
		{
			int typPoprzedniegoPola = (int)typPola.puste;
			foreach (var tPoleObecne in zbitePionki)
			{
				if (pionekDoRuchu.rodzaj == (int)typPola.czarnaKrolowa		// czarne krolowe nie zbijaja czarnych pionkow/krolow
					&& (tPoleObecne == (int)typPola.czarnaKrolowa || tPoleObecne == (int)typPola.czarnyPionek)) return false;

				if (pionekDoRuchu.rodzaj == (int)typPola.bialaKrolowa       // biale krolowe nie zbijaja bialych pionkow/krolow
					&& (tPoleObecne == (int)typPola.bialaKrolowa || tPoleObecne == (int)typPola.bialyPionek)) return false;

				if (typPoprzedniegoPola != (int)typPola.puste && tPoleObecne != (int)typPola.puste) return false;
				typPoprzedniegoPola = tPoleObecne;      // gdy 2pionki obok siebie i nie mozna zbic
			}
			return true;
		}
	}
}


