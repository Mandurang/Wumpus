﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WumpusWorld.MapObject
{
    public class Bat : MapObject
    {
        public const char symbol = 'B';
        public Bat(int X, int Y)
        {
            X = X;
            Y = Y;
        }
        public Bat() { }
    }
}
