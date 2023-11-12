using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WumpusWorld.MapObject
{
    public class Wumpus : MapObject
    {
        public const char symbol = 'W';
        public Random random = new Random();
        public Wumpus(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }
        public Wumpus() { }

        public void RandomMoveWumpus(Map map)
        {
            int newX = X;
            int newY = Y;
            int chanceMoveWumpus = random.Next(1, 5);

            if (chanceMoveWumpus != 1)
            {
                do
                {
                    newX = X;
                    newY = Y;
                    int randomMove = random.Next(1, 5);
                    switch (randomMove)
                    {
                        case 1:
                            newX = X - 1;
                            break;
                        case 2:
                            newY = Y - 1;
                            break;
                        case 3:
                            newX = X + 1;
                            break;
                        case 4:
                            newY = Y + 1;
                            break;
                    }
                } while (!IsValid(newX, newY, map));
                X = newX;
                Y = newY;
            }
        }

        private bool IsValid(int x, int y, Map map)
        {
            return x >= 0 && x < map.Size && y >= 0 && y < map.Size;
        }
    }
}
