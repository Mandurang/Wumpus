using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wumpus
{
    public class Wumpus : MapObject
    {
        public int WumpusX { get; set; }
        public int WumpusY { get; set; }

        public Wumpus(int wumpusX, int wumpusY)
        {
            WumpusX = wumpusX;
            WumpusY = wumpusY;
        }
        public Wumpus() { }

    }
}
