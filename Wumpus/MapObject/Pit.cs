using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WumpusWorld.MapObject
{
    public class Pit : MapObject
    {
        public const char symbol = 'P';
        public Pit(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }
        public Pit() { }
    }
}


