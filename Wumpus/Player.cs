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

        public void Move(int newX, int newY)
        {
            X = newX;
            Y = newY;
        }

        public void ShootArrow(int directionX, int directionY, WumpusWorldGame world)
        {
            int targetX = X;
            int targetY = Y;

            while (world.GetTile(targetX, targetY).Content != '_')
            {
                if (world.GetTile(targetX, targetY).Content == 'W')
                {
                    Console.WriteLine("Congratulations! You shot the Wumpus and won the game.");
                    Environment.Exit(0);
                }
                targetX += directionX;
                targetY += directionY;
            }

            Console.WriteLine("You missed.  The arrow hit a wall.");
        }
    }
}