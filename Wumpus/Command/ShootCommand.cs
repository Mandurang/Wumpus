using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WumpusWorld.MapObject;

namespace WumpusWorld.Command
{
    public class ShootCommand : ICommand, IWumpusEncountered
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
            int wumpusX = wumpusWorldGame.Wumpus.X;
            int wumpusY = wumpusWorldGame.Wumpus.Y;

            if (wumpusWorldGame.Player.QuantityArrow > 0)
            {
                wumpusWorldGame.Player.QuantityArrow--;
                Encountered(directionX, directionY, wumpusX, wumpusY);
            }
            else
            {
                Console.Write("Out of arrows!");
            }
        }

        public void Encountered(int directionX, int directionY, int wumpusX, int wumpusY)
        {
            if (directionX == wumpusX && directionY == wumpusY)
            {
                Console.Write("Congratulations! You shot the Wumpus and won the game.");
                Environment.Exit(0);
            }
            else
            {
                Console.Write("You missed. The arrow hit a wall.");
            }
        }
    }
}
