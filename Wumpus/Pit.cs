using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Wumpus
{
    public class Pit : MapObject
    {
        public int PitX { get; set; }
        public int PitY { get; set; }

        public Pit(int pitX, int pitY)
        {
            PitX = pitX;
            PitY = pitY;
        }
        public Pit() { }
    }
}


