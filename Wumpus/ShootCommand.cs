using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wumpus
{
    public class ShootCommand : ICommand
    {
        private Player player;
        private Map map;
        private int directionX, directionY;

        public ShootCommand(Player player, Map map, int directionX, int directionY)
        {
            this.player = player;
            this.map = map;
            this.directionX = directionX;
            this.directionY = directionY;
        }

        public void Execute()
        {
            ShootArrow(directionX, directionY, map);
        }

        private void ShootArrow(int directionX, int directionY, Map map)
        {
            int targetX = player.X;
            int targetY = player.Y;
            if (player.QuantityArrow > 0)
            {
                player.QuantityArrow--;

                for (int i = 0; i < 1; i++)
                {
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
            }
            else
            {
                Console.WriteLine("Out of arrows!");
            }
        }
    }
}
