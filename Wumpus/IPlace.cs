﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wumpus
{
    public interface IPlace<T>
    {
        List<T> Place(int quantityTreasure, Random random, char[][] MapSquare, int worldSize);
    }
}