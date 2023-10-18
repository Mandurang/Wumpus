using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wumpus
{
    public class Bet : MapObject
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Bet(int X, int Y)
        {
            X = X;
            Y = Y;
        }
        public Bet() { }
    }
}
