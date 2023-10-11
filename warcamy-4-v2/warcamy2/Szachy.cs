using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace warcamy2
{
    internal static class Szachy
    {
        public static List<Pole> pola = new List<Pole>();
        
        public static bool czyCiemnePole(int wspX, int wspY)
        {
            if ((wspX+wspY)%2 == 1)  return true; 
            else return false;
        }
    }
}
