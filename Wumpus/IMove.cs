using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wumpus
{
    public class Move
    {
        public void MovePlayer(int newX, int newY)
        {
            GameLoop gameLoop = new GameLoop();

            var visitedWorld = gameLoop.isVisitedWorld();
            var createdWorld = gameLoop.CreateWorld();
            if (!IsValid(newX, newY) || visitedWorld[newX][newY])
            {
                Console.WriteLine("Invalid move.");
                return;
            }

            visitedWorld[gameLoop.playerX][gameLoop.playerY] = true;
            gameLoop.playerX = newX;
            gameLoop.playerY = newY;

            if (createdWorld[gameLoop.playerX][gameLoop.playerY] == '@' || createdWorld[gameLoop.playerX][gameLoop.playerY] == 'W')
            {
                Console.WriteLine("Game over! You fell into a pit or encountered the Wumpus.");
                Environment.Exit(0);
            }

            gameLoop.PrintWorld();
        }

        public bool IsValid(int x, int y)
        {
            return x >= 0 && x < 4 && y >= 0 && y < 4;
        }
    }
    
    internal interface IMove
    {
    }
}
