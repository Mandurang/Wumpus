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
        private WumpusWorldGame _wumpusWorldGame;
        private int directionX, directionY;
        private Direction? _direction;

        public ShootCommand(WumpusWorldGame wumpusWorldGame, int directionX, int directionY)
        {
            _wumpusWorldGame = wumpusWorldGame;
            this.directionX = directionX;
            this.directionY = directionY;
        }

        public ShootCommand(WumpusWorldGame wumpusWorldGame, Direction? direction)
        {
            _wumpusWorldGame = wumpusWorldGame;
            _direction = direction;
        }

        public void Execute()
        {
            ShootArrowV2(_direction, _wumpusWorldGame);
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

        private void ShootArrowV2(Direction? direction, WumpusWorldGame wumpusWorldGame)
        {
            var (dx, dy) = _direction switch
            {
                Direction.Up => (-1, 0),
                Direction.Down => (1, 0),
                Direction.Left => (0, -1),
                Direction.Right => (0, 1),
            };
            int playerX = wumpusWorldGame.Player.X;
            int playerY = wumpusWorldGame.Player.Y;
            int newX = playerX + dx;
            int newY = playerY + dy;

            int wumpusX = wumpusWorldGame.Wumpus.X;
            int wumpusY = wumpusWorldGame.Wumpus.Y;

           

            if (wumpusWorldGame.Player.QuantityArrow > 0)
            {
                wumpusWorldGame.Player.QuantityArrow--;
                EncounteredV2(newX, newY, wumpusX, wumpusY);
            }
            else
            {
                Console.Write("Out of arrows!");
            }
        }

        public void EncounteredV2(int directionX, int directionY, int wumpusX, int wumpusY)
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
