using Wumpus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wumpus
{
    public class Treasure : MapObject
    {
        public const char symbol = 'T';
        public Treasure(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public Treasure() { }
    }
}
