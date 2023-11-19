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
        private ValidService validSerivice = new ValidService();
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
            wumpusWorld.CheckForBatsSound(); // Проверка запаха Wumpus после перемещения игрока.

            do
            {
                Console.Write("Enter your move (W/A/S/D) or 'F' to shoot: ");
                char move = Console.ReadKey().KeyChar;
                Console.WriteLine();

                if (move == 'F')
                {
                    Console.Write("Enter the direction to shoot (W/A/S/D): ");
                    char shootDirection = Console.ReadKey().KeyChar;
                    int targetX = wumpusWorld.Player.X;
                    int targetY = wumpusWorld.Player.Y;

                    var destinationRoom = ExecuteDirection(shootDirection, targetX, targetY);
                    targetX = destinationRoom.X;
                    targetY = destinationRoom.Y;

                    ICommand shootCommand = new ShootCommand(wumpusWorld, targetX, targetY);
                    wumpusWorld.ExecuteCommand(shootCommand);

                }
                else
                {
                    int newX = wumpusWorld.Player.X;
                    int newY = wumpusWorld.Player.Y;

                    var destinationRoom = ExecuteDirection(move, newX, newY);

                    newX = destinationRoom.X;
                    newY = destinationRoom.Y;

                    if (!validSerivice.IsValid(newX, newY, wumpusWorld.Map.Size))
                    {
                        Console.WriteLine("Invalid move. You can't go outside the map.");
                        continue;
                    }

                    ICommand movePlayer = new MoveCommand(wumpusWorld.Player, newX, newY, wumpusWorld.Map);
                    wumpusWorld.ExecuteCommand(movePlayer);
                }
            } while (true);
        }

        private Room ExecuteDirection(char move, int directionX, int directionY)
        {
            Room room = new Room { X = directionX, Y = directionY };

            bool validMove = true;

            do
            {
                switch (move)
                {
                    case 'W':
                        room.X--;
                        validMove = true;
                        break;
                    case 'A':
                        room.Y--;
                        validMove = true;
                        break;
                    case 'S':
                        room.X++;
                        validMove = true;
                        break;
                    case 'D':
                        room.Y++;
                        validMove = true;
                        break;
                    default:
                        Console.WriteLine("Invalid move. Use W/A/S/D to move.");
                        validMove = false;
                        break;
                }

                if (!validMove)
                {
                    Console.Write("Enter a valid move: ");
                    move = Console.ReadKey().KeyChar;
                    Console.WriteLine();
                }

            } while (!validMove);

            return room;
        }

        private Direction? ExecuteDirectionV2(char move)
        {
            return move switch
            {
                'W' => Direction.Up,
                'S' => Direction.Down,
                'A' => Direction.Left,
                'D' => Direction.Right,
                _ => null
            };
        }

        public void RunV2()
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
            wumpusWorld.CheckForBatsSound(); // Проверка запаха Wumpus после перемещения игрока.

            do
            {
                Console.Write("Enter your move (W/A/S/D) or 'F' to shoot: ");
                char move = Console.ReadKey().KeyChar;
                Console.WriteLine();

                if (move == 'F')
                {
                    Console.Write("Enter the direction to shoot (W/A/S/D): ");
                    char shootDirection = Console.ReadKey().KeyChar;

                    var direction = ExecuteDirectionV2(shootDirection);

                    //ICommand shootCommand = new ShootCommand(wumpusWorld, direction);
                    //wumpusWorld.ExecuteCommand(shootCommand);

                }
                else
                {
                    var direction = ExecuteDirectionV2(move);

                    //if(direction is null)
                    //{
                    //    Console.WriteLine("Invalid move. You can't go outside the map.");
                    //    continue;
                    //}

                    ICommand movePlayer = new MoveCommand(wumpusWorld.Player, direction, wumpusWorld.Map);
                    wumpusWorld.ExecuteCommand(movePlayer);
                }
            } while (true);
        }

        public void RunV3()
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
            wumpusWorld.CheckForBatsSound(); // Проверка запаха Wumpus после перемещения игрока.

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

    //public enum Direction
    //{
    //    Up,
    //    Down,
    //    Left,
    //    Right
    //}
    //var direction = ExecuteDirectionV2(shootDirection);

    //ICommand shootCommand = new ShootCommand(wumpusWorld, direction);
    //wumpusWorld.ExecuteCommand(shootCommand);


    //var direction = ExecuteDirectionV2(inputUserDirection);

    //if(direction is null)
    //{
    //    Console.WriteLine("Invalid move. You can't go outside the map.");
    //    continue;
    //}
}
