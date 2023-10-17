using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wumpus
{
    public class Wumpus
    {
        public int WumpusX { get; set; }
        public int WumpusY { get; set; }

        public Wumpus(int wumpusX, int wumpusY)
        {
            WumpusX = wumpusX;
            WumpusY = wumpusY;
        }
        public Wumpus() { }

        public List<Wumpus> PlaceWumpus(int quantityWumpus, Random random, char[][] MapSquare, int worldSize)
        {
            List<Wumpus> wumpus = new List<Wumpus>();

            for (int i = 0; i < quantityWumpus; i++)
            {
                int wumpusX, wumpusY;

                do
                {
                    wumpusX = random.Next(worldSize);
                    wumpusY = random.Next(worldSize);
                } while (MapSquare[wumpusX][wumpusY] != '_');
                MapSquare[wumpusX][wumpusY] = 'W';

                wumpus.Add(new Wumpus(wumpusX, wumpusY));

            }
            return wumpus;
        }

    }
}
