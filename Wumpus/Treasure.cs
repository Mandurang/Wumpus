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
        public int TreasureX { get; set; }
        public int TreasureY { get; set; }

        public Treasure(int treasureX, int treasureY)
        {
            TreasureX = treasureX;
            TreasureY = treasureY;
        }

        public Treasure() { }

        
    }
}
