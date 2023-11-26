using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WumpusWorld.MapObject
{
    public class Player : MapObject
    {
        public const char symbol = '@';
        public int QuantityArrow { get; set; } = 4;

        public Random random = new Random();

        public Player()
        {

        }

        public Player(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
