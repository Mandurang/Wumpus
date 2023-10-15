using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Treasure
    {
        public int TreasureX { get; set; }
        public int TreasureY { get; set; }

        public Treasure(int treasureX, int treasureY)
        {
            TreasureX = treasureX;
            TreasureY = treasureY;
        }

        public Treasure() { }

        public List<Treasure> PlaceTreasure(int quantityTreasure, Random random, char[][] MapSquare, int worldSize)
        {
            List<Treasure> treasures = new List<Treasure>();

            for (int i = 0; i < quantityTreasure; i++)
            {
                int treasureX, treasureY;

                do
                {
                    treasureX = random.Next(worldSize);
                    treasureY = random.Next(worldSize);
                } while (MapSquare[treasureX][treasureY] != '_');
                MapSquare[treasureX][treasureY] = 'G';

                treasures.Add(new Treasure(treasureX, treasureY));

            }
            return treasures;
        }
    }
}
