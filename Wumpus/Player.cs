using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wumpus
{
    public class Player : MapObject
    {
        public const char symbol = '@';
        public int QuantityArrow { get; set; } = 4;

        public Random random = new Random();

        public Player(int x, int y)
        {
            X = x; 
            Y = y;
        }

        public void Move(int newX, int newY)
        {
            X = newX;
            Y = newY;
        }

        public void ShootArrow(int directionX, int directionY, Map map)
        {
            int targetX = X;
            int targetY = Y;
            if (QuantityArrow > 0)
            {
                QuantityArrow--;
                while (map.GetTile(targetX, targetY).Content != '_')
                {
                    if (map.GetTile(targetX, targetY).Content == 'W')
                    {
                        Console.WriteLine("Congratulations! You shot the Wumpus and won the game.");
                        Environment.Exit(0);
                    }
                    targetX += directionX;
                    targetY += directionY;
                }

                Console.WriteLine("You missed.  The arrow hit a wall.");
            }
            else
            {
                Console.WriteLine("Out of arrows!");
            }
        }


        public void MovePlayer(int newX, int newY, Map map)
        {
            if (!map.IsValid(newX, newY))
            {
                Console.WriteLine("Invalid move.");
                return;
            }

            

            X = newX; Y = newY;

            if (map.MapSquare[X,Y].Content == 'P')
            {
                Console.WriteLine("Game over! You fell into a pit.");
                Environment.Exit(0);
            }

            if (map.MapSquare[X, Y].Content == 'W')
            {
                Console.WriteLine("Game over! Encountered the Wumpus.");
                Environment.Exit(0);
            }

            if (map.MapSquare[X, Y].Content == 'B')
            {
                Console.WriteLine("Go to! Encountered the Bet.");
                X = random.Next(map.MapSquare.GetLength(0));
                Y = random.Next(map.MapSquare.GetLength(0));
            }

            else if (map.MapSquare[X, Y].Content == 'G')
            {
                Console.WriteLine("Congratulations! You found the treasure and won the game.");
                Environment.Exit(0);
            }

            PrintWorld();
            CheckForWumpusSmell(); // Проверка запаха Wumpus после перемещения игрока.
            CheckForPitWind();    // Проверка ветра (яма) после перемещения игрока.
            CheckForBatsSound();
        }

        //public void ShootArrow(int directionX, int directionY, Map map)
        //{
        //    if (QuantityArrow > 0)
        //    {
        //        QuantityArrow--;
        //        Console.WriteLine(" - Direction. You shoot an arrow!");

        //        int x = X;
        //        int y = Y;

        //        for (int i = 0; i < 1; i++)
        //        {
        //            x += directionX;
        //            y += directionY;

        //            if (map.MapSquare[X, Y].Content == 'W')
        //            {
        //                Console.WriteLine("Congratulations! You shot the Wumpus and won the game.");
        //                Environment.Exit(0);
        //            }
        //        }

        //        Console.WriteLine("You missed. The Wumpus is still alive.");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Out of arrows!");
        //    }
        //}
    }
}
