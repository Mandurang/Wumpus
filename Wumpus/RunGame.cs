using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using WumpusWorld.Command;
using static WumpusWorld.RunGame;
using WumpusWorld.MapObject;

namespace WumpusWorld
{
    public class RunGame
    {
        private UserInputService userInputService = new UserInputService();
        public void Run()
        {
            Console.WriteLine("Welcome to Wumpus World!");
            Console.WriteLine("Legend: ? - Unexplored, _ - Explored, P - Player, P - Pit, W - Wumpus, B - Bats, T - Treasure");

            WumpusWorldGame wumpusWorld = new WumpusWorldGame();
            wumpusWorld.SetQuantityPits();
            wumpusWorld.SetQuantityTreasures();
            wumpusWorld.SetQuantityBats();
            wumpusWorld.GenerateWorld(); // Генерация случайного мира.
            wumpusWorld.PrintWorld();
            wumpusWorld.CheckForWumpusSmell(); // Проверка запаха Wumpus при старте игры.
            wumpusWorld.CheckForPitWind();    // Проверка драфта (яма) при старте игры.
            wumpusWorld.CheckForBatsSound(); // Проверка запаха Wumpus Aпосле перемещения игрока.

            do
            {
                Console.Write("Enter your move (W/A/S/D) or press 'Alt' to shoot: ");
                ConsoleKeyInfo move = Console.ReadKey();
                Console.WriteLine();

                if (move.Modifiers == ConsoleModifiers.Alt)
                {
                    var inputUserDirection = userInputService.GetValidUserMoveInput(move.KeyChar);

                    ICommand shootCommand = new ShootCommand(wumpusWorld, inputUserDirection);
                    wumpusWorld.ExecuteCommand(shootCommand);
                }
                else
                {
                    var inputUserDirection = userInputService.GetValidUserMoveInput(move.KeyChar);

                    ICommand movePlayer = new MoveCommand(wumpusWorld.Player, inputUserDirection, wumpusWorld.Map);
                    wumpusWorld.ExecuteCommand(movePlayer);
                }
            } while (true);
        }
    }
}
