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
        public Wumpus(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }
        public Wumpus() { }

        public void RandomMoveWumpus()
        {
            foreach (var wumpus in Wumpuses)
            {
                int newX = wumpus.X;
                int newY = wumpus.Y;
                int chanceMoveWumpus = random.Next(1, 5);

                if (chanceMoveWumpus != 1)
                {
                    do
                    {
                        newX = wumpus.X;
                        newY = wumpus.Y;
                        int randomMove = random.Next(1, 5);
                        switch (randomMove)
                        {
                            case 1:
                                newX = wumpus.X - 1;
                                break;
                            case 2:
                                newY = wumpus.Y - 1;
                                break;
                            case 3:
                                newX = wumpus.X + 1;
                                break;
                            case 4:
                                newY = wumpus.Y + 1;
                                break;
                        }
                    } while (!IsValidMapForWumpus(newX, newY));
                    MapSquare[wumpus.X][wumpus.Y] = '_';
                    wumpus.X = newX;
                    wumpus.Y = newY;
                    MapSquare[wumpus.X][wumpus.Y] = 'W';
                }
            }
        }
    }
}
