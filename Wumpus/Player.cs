﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wumpus
{
    public class Player : MapObject
    {
        public const char symbol = '@';
        public int QuantityArrow { get; set; } = 4;
    }
}