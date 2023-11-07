using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WumpusWorld.MapObject;

namespace WumpusWorld.Command
{
    public class ShootCommand : ICommand
    {
        private WumpusWorldGame wumpusWorldGame;
        private int directionX, directionY;

        public ShootCommand(WumpusWorldGame wumpusWorldGame, int directionX, int directionY)
        {
            this.wumpusWorldGame = wumpusWorldGame;
            this.directionX = directionX;
            this.directionY = directionY;
        }

        public void Execute()
        {
            ShootArrow(directionX, directionY, wumpusWorldGame);
        }

        private void ShootArrow(int directionX, int directionY, WumpusWorldGame wumpusWorldGame)
        {
            int targetX = wumpusWorldGame.Player.X;
            int targetY = wumpusWorldGame.Player.Y;

            int wumpusX = wumpusWorldGame.Wumpus.X;
            int wumpusY = wumpusWorldGame.Wumpus.Y;

            if (wumpusWorldGame.Player.QuantityArrow > 0)
            {
                wumpusWorldGame.Player.QuantityArrow--;

                for (int i = 0; i < 1; i++)
                {
                    while (wumpusWorldGame.Map.GetRoom(targetX, targetY).Content != '_')
                    {
                        if (wumpusWorldGame.Map.GetRoom(targetX, targetY) == wumpusWorldGame.Map.GetRoom(wumpusX, wumpusY))
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
