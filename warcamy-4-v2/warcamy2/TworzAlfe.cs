using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warcamy2
{
    internal static class TworzAlfe
    {
        public static Image dodajAlfa(Image inObr)
        {
            Bitmap bm = new Bitmap(inObr);

            for (int x = 0; x < bm.Width; x++)
            {
                for (int y = 0; y < bm.Height; y++)
                {
                    Color pixelColor = bm.GetPixel(x, y);
                    if (pixelColor.R == 255 && pixelColor.G == 255 && pixelColor.B == 255) // Kolor biały
                    {
                        bm.SetPixel(x, y, Color.FromArgb(0, pixelColor)); // Ustawienie przezroczystej alfy
                    }
                }
            }

            return (Image)bm;
        }
    }
}
