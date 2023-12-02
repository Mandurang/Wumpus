using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WumpusWorld.MapObject
{
    public class Wumpus : MapObject
    {
        public const char symbol = 'W';
        public Random random = new Random();

        public Wumpus(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }
        public Wumpus() { }
    }
}
