using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace warcamy2
{
    enum typPola { puste=1, czarnyPionek=2, czarnaKrolowa=3, bialyPionek=4, bialaKrolowa=5 };

    public partial class Form1 : Form
    {
        static bool ruchCzyCzarne = true;
        static Pole pionekDoRuchu = new Pole((int)typPola.czarnyPionek);
		static bool kontynuujRuch = false;
		public Form1()
        {
            InitializeComponent();
            
            Pole pole;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if ((i + j) % 2 == 1)
                    {
                        if (j < 1)
                        {
                            pole = new Pole(i, j, 50, 50, ParColor.ciemnePole, (int)typPola.czarnaKrolowa);
                        }
                        else if (j > 6)
                        {
                            pole = new Pole(i, j, 50, 50, ParColor.ciemnePole, (int)typPola.bialaKrolowa);
                        }
                        //////////////////////////////// potem usunac
						else if (j == 1)
						{
							pole = new Pole(i, j, 50, 50, ParColor.ciemnePole, (int)typPola.czarnyPionek);
						}
						else if (j == 6)
						{
							pole = new Pole(i, j, 50, 50, ParColor.ciemnePole, (int)typPola.bialyPionek);
						}
                        ///////////////////////////////////
						else
                        {
                            pole = new Pole(i, j, 50, 50, ParColor.ciemnePole, (int)typPola.puste);
                        }
                    }
                    else
                    {
                        pole = new Pole(i, j, 50, 50, ParColor.jasnePole, (int)typPola.puste);
                    }

                    Szachy.pola.Add(pole);
                    this.Controls.Add(pole);
                    pole.MouseEnter += ruchMyszy;
                    pole.MouseLeave += zostMysz;
                    pole.MouseClick += klikMyszy;
                }
            }
        }

        private void klikMyszy(object sender, MouseEventArgs e)
        {
            Pole pole = sender as Pole;

			if (Szachy.czyCiemnePole(pole.wspX, pole.wspY))     // ruch tylko ciemne pola szachownicy
            {
                #region Ruchy dla czarnych kolorów
                if (ruchCzyCzarne == true)
                {
                    if (kontynuujRuch == false && (pole.rodzaj == (int)typPola.czarnyPionek || pole.rodzaj == (int)typPola.czarnaKrolowa))   // gdy pionek/krolowa jest wybrany
                    {
                        pionekDoRuchu = pole;
                        PodswietlaniePola.resetujKolorySzachownicy();
						PodswietlaniePola.ruchPionkaNaPuste(pionekDoRuchu, !kontynuujRuch);
					}
                    else if (pole.rodzaj == (int)typPola.puste && pionekDoRuchu.rodzaj == (int)typPola.czarnyPionek
                        && WarunkiPionkow.pionekNaPuste(pole, pionekDoRuchu))
                    // gdy przemieszczamy pionka na puste pole
                    {
                        if (RuchyPionkow.ruchPionkaNaPuste(pole, pionekDoRuchu)) kontynuujRuch = true;
						else {ruchCzyCzarne = false; kontynuujRuch = false;}
						RuchyPionkow.SprawdzZamienNaKrolawa(pionekDoRuchu);
						PodswietlaniePola.resetujKolorySzachownicy();
                        if (kontynuujRuch) PodswietlaniePola.ruchPionkaNaPuste(pionekDoRuchu, kontynuujRuch); ;
					}
                    else if (pole.rodzaj == (int)typPola.puste && pionekDoRuchu.rodzaj == (int)typPola.czarnaKrolowa
                        && WarunkiPionkow.krolowaNaPuste(pole, pionekDoRuchu))
                    // gdy zbijamy pionka królową / wykonujemy ruch
                    {
						if (RuchyPionkow.ruchKrolowyNaPuste(pole, pionekDoRuchu)) kontynuujRuch = true;
						else { ruchCzyCzarne = false; kontynuujRuch = false; }
                        PodswietlaniePola.resetujKolorySzachownicy();
                        if (kontynuujRuch) PodswietlaniePola.ruchPionkaNaPuste(pionekDoRuchu, kontynuujRuch);
					}
                }
				#endregion
				else   // dla ruchów białych pionków
                #region Ruchy dla białych kolorów
                {
                    if (kontynuujRuch == false && (pole.rodzaj == (int)typPola.bialyPionek || pole.rodzaj == (int)typPola.bialaKrolowa))   // gdy pionek/krolowa jest wybrany
                    {
                        pionekDoRuchu = pole;
						PodswietlaniePola.resetujKolorySzachownicy();
						PodswietlaniePola.ruchPionkaNaPuste(pionekDoRuchu, !kontynuujRuch);
					}
                    else if (pole.rodzaj == (int)typPola.puste && pionekDoRuchu.rodzaj == (int)typPola.bialyPionek
                        && WarunkiPionkow.pionekNaPuste(pole, pionekDoRuchu))
                    // gdy przemieszczamy pionka na puste pole
                    {
						if (RuchyPionkow.ruchPionkaNaPuste(pole, pionekDoRuchu)) kontynuujRuch = true;
						else { ruchCzyCzarne = true; kontynuujRuch = false; }
						RuchyPionkow.SprawdzZamienNaKrolawa(pionekDoRuchu);
						PodswietlaniePola.resetujKolorySzachownicy();
						if (kontynuujRuch) PodswietlaniePola.ruchPionkaNaPuste(pionekDoRuchu, kontynuujRuch);
					}
                    else if (pole.rodzaj == (int)typPola.puste && pionekDoRuchu.rodzaj == (int)typPola.bialaKrolowa
                        && WarunkiPionkow.krolowaNaPuste(pole, pionekDoRuchu))
					// gdy zbijamy pionka królową / wykonujemy ruch
					{
						if (RuchyPionkow.ruchKrolowyNaPuste(pole, pionekDoRuchu)) kontynuujRuch = true;
                        else { ruchCzyCzarne = true; kontynuujRuch = false; }
                        PodswietlaniePola.resetujKolorySzachownicy();
						if (kontynuujRuch) PodswietlaniePola.ruchPionkaNaPuste(pionekDoRuchu, kontynuujRuch);
					}
                }
                #endregion
            }

        }
            
        private void zostMysz(object sender, EventArgs e)
        {
            Pole pole = sender as Pole;
           //pole.resetKolor();
        }

        private void ruchMyszy(object sender, EventArgs e)
        {
            Pole pole = sender as Pole;
            //pole.ustawKolor(Color.Green);
        }
    }
}
