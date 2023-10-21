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
            Console.WriteLine("Legend: ? - Unexplored, _ - Empty, P - Player, P - Pit, W - Wumpus, B - Bets, G - Gold");
            wumpusWorld.SetWorldSize();
            wumpusWorld.SetQuantityWupus();
            wumpusWorld.SetQuantityPits();
            wumpusWorld.SetQuantityTreasure();
            wumpusWorld.SetQuantityBet();
            wumpusWorld.GenerateWorld(); // Генерация случайного мира.
            wumpusWorld.PrintWorld();
            wumpusWorld.CheckForWumpusSmell(); // Проверка запаха Wumpus при старте игры.
            wumpusWorld.CheckForPitWind();    // Проверка драфта (яма) при старте игры.
            wumpusWorld.CheckForBetsSound();

            while (true)
            {
                

                Console.Write("Enter your move (W/A/S/D) or 'F' to shoot: ");
                char move = Console.ReadKey().KeyChar;
                Console.WriteLine();

                if (move == 'F')
                {
                    Console.Write("Enter the direction to shoot (W/A/S/D): ");
                    char shootDirection = Console.ReadKey().KeyChar;
                    int directionX = 0;
                    int directionY = 0;

                    switch (shootDirection)
                    {
                        case 'W':
                            directionX = -1;
                            break;
                        case 'A':
                            directionY = -1;
                            break;
                        case 'S':
                            directionX = 1;
                            break;
                        case 'D':
                            directionY = 1;
                            break;
                        default:
                            Console.WriteLine("Invalid direction. Use W/A/S/D to shoot.");
                            continue;
                    }

                    wumpusWorld.ShootArrow(directionX, directionY);
                    continue;
                }

                int newX = wumpusWorld.Player.X;
                int newY = wumpusWorld.Player.Y;

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
                    case 'D' :
                        newY++;
                        break;
                    default:
                        Console.WriteLine("Invalid move. Use W/A/S/D to move.");
                        continue;
                }
                wumpusWorld.RandomMoveWumpus();
                wumpusWorld.MovePlayer(newX, newY);
            }
        }
    }
}
