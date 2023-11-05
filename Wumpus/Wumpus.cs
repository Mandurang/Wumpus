using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wumpus
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
                } while (!IsValidMapForWumpus(newX, newY, map));
                map.MapSquare[X, Y].Content = '_';
                X = newX;
                Y = newY;
                map.MapSquare[X, Y].Content = 'W';
            }
        }

        public bool IsValid(int x, int y, Map map)
        {
            return x >= 0 && x < map.MapSquare.GetLength(0) && y >= 0 && y < map.MapSquare.GetLength(1);
        }

        private bool IsValidMapForWumpus(int x, int y, Map map)
        {
            if (IsValid(x, y, map))
            {
                if (map.MapSquare[x, y].Content == '_')
                {
                    return x >= 0 && x < map.MapSquare.GetLength(0) && y >= 0 && y < map.MapSquare.GetLength(1);
                }
            }

            return false;
        }
    }
}
