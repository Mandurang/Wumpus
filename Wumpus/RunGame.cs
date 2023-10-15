using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wumpus
{
    public class RunGame
    {
        public void Run()
        {
            WumpusWorldGame wumpusWorld = new WumpusWorldGame();
            Console.WriteLine("Welcome to Wumpus World!");
            Console.WriteLine("Legend: ? - Unexplored, _ - Empty, @ - Player, P - Pit, W - Wumpus, G - Gold");
            wumpusWorld.SetWorldSize();
            wumpusWorld.SetQuantityPits();
            wumpusWorld.SetQuantityTreasure();
            wumpusWorld.GenerateWorld(); // Генерация случайного мира.
            wumpusWorld.PrintWorld();
            wumpusWorld.CheckForWumpusSmell(); // Проверка запаха Wumpus при старте игры.
            wumpusWorld.CheckForPitWind();    // Проверка драфта (яма) при старте игры.

            while (true)
            {
                Console.Write("Enter your move (W/A/S/D): ");
                char move = Console.ReadKey().KeyChar;
                Console.WriteLine();

                int newX = wumpusWorld.playerX;
                int newY = wumpusWorld.playerY;

                switch (move)
                {
                    case 'W':
                        newX--;
                        break;
                    case 'A':
                        newY--;
                        break;
                    case 'S':
                        newX++;
                        break;
                    case 'D':
                        newY++;
                        break;
                    default:
                        Console.WriteLine("Invalid move. Use W/A/S/D to move.");
                        continue;
                }

                wumpusWorld.MovePlayer(newX, newY);
            }
        }
    }
}
