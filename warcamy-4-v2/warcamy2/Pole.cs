using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace warcamy2
{
    public class Pole : PictureBox, ICloneable
    {
        public int wspX, wspY;
        public int rodzaj = 0;
        Color domyslKolorPola;
        Color zaznaczonyPionek = Color.OrangeRed;
        Image imagePola;
        public static Image obrCzarnyPionek = TworzAlfe.dodajAlfa(Image.FromFile($@"{AppDomain.CurrentDomain.BaseDirectory}..\..\obrazy\pionekC.png"));
		public static Image obrBialyPionek = TworzAlfe.dodajAlfa(Image.FromFile($@"{AppDomain.CurrentDomain.BaseDirectory}..\..\obrazy\pionekB.png"));
		public static Image obrCzarnaKrolowa = TworzAlfe.dodajAlfa(Image.FromFile($@"{AppDomain.CurrentDomain.BaseDirectory}..\..\obrazy\pionekCK.png"));
		public static Image obrBialaKrolowa = TworzAlfe.dodajAlfa(Image.FromFile($@"{AppDomain.CurrentDomain.BaseDirectory}..\..\obrazy\pionekBK.png"));

        public Pole(int rodzaj)
        {
			this.rodzaj = rodzaj;
		}
        public Pole(int wspX, int wspY, int Width, int Height, Color BackColor, int rodzaj)
        {
            SizeMode = PictureBoxSizeMode.StretchImage;
            this.Width = Width;
            this.Height = Height;
            RuchyPionkow.Width = Width;
            RuchyPionkow.Height = Height;
            this.wspX = wspX;
            this.wspY = wspY;
            int aktualX = wspX * (Width + 3);
            int aktualY = wspY * (Height + 3);
            Location = new Point(aktualX, aktualY);
            domyslKolorPola = BackColor;
            this.BackColor = BackColor;
            this.rodzaj = rodzaj;

            if (rodzaj == (int)typPola.czarnyPionek) this.Image = obrCzarnyPionek;
            else if (rodzaj == (int)typPola.bialyPionek) this.Image = obrBialyPionek;
            else if (rodzaj == (int)typPola.czarnaKrolowa) this.Image = obrCzarnaKrolowa;
            else if (rodzaj == (int)typPola.bialaKrolowa) this.Image = obrBialaKrolowa;
            imagePola = this.Image;
        }
        
        public void resetKolor()
        {
            BackColor = domyslKolorPola;
        }

        public void ustawKolor(Color kolor)
        {
            BackColor = kolor;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
