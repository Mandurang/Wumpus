﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Wumpus
{
    public class Pit : MapObject
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Pit(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }
        public Pit() { }
    }
}


