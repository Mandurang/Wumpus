using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wumpus
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace ConsoleApp1
    {
        public class Pit
        {
            public int PitX { get; set; }
            public int PitY { get; set; }

            public Pit(int pitX, int pitY)
            {
                PitX = pitX;
                PitY = pitY;
            }
            public Pit() { }

            public List<Pit> PlacePits(int quantityPit, Random random, char[][] MapSquare, int worldSize)
            {
                List<Pit> pits = new List<Pit>();

                for (int i = 0; i < quantityPit; i++)
                {
                    int pitX, pitY;

                    do
                    {
                        pitX = random.Next(worldSize);
                        pitY = random.Next(worldSize);
                    } while (MapSquare[pitX][pitY] != '_');
                    MapSquare[pitX][pitY] = 'P';

                    pits.Add(new Pit(pitX, pitY));

                }
                return pits;
            }
        }
    }

}
