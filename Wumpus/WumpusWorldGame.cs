using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WumpusWorld.Command;
using WumpusWorld.MapObject;
using static WumpusWorld.RunGame;

namespace WumpusWorld
{
    public class WumpusWorldGame : IWumpusEncountered
    {
        public Map Map { get; }
        public int QuantityPits { get; set; }
        public int QuantityTreasure { get; set; }
        public int QuantityBats { get; set; }
        public List<Treasure> Treasures { get; set; }
        public List<Pit> Pits { get; set; }
        public List<Bat> Bats { get; set; }
        public Player Player { get; set; }
        public Wumpus Wumpus { get; set; }

        private bool wumpusSmell = false; // Флаг для запаха Wumpus.
        private bool pitWind = false;     // Флаг для драфта (яма).
        private bool betSound = false;     // Флаг для скрежита крильев (bet).

        private Random random = new Random();

        private Placer placer = new Placer();

        private List<ICommand> commandHistory = new List<ICommand>();

        private UserInputService userInputService = new UserInputService();
        private ValidService validSerivice = new ValidService();

        public WumpusWorldGame()
        {
            Map = new Map();
            Player = new Player();
        }

        public void GenerateWorld()
        {
            Pits = placer.PlacePits(QuantityPits, random, Map.MapSquare);

            Treasures = placer.PlaceTreasures(QuantityTreasure, random, Map.MapSquare);

            Bats = placer.PlaceBats(QuantityBats, random, Map.MapSquare);

            Wumpus = placer.PlaceWumpus(random, Map.MapSquare);

            Player = placer.PlacePlayer(random, Map.MapSquare);
        }

        public void PrintWorld()
        {
            Console.Clear();

            int mapSize = Map.MapSquare.GetLength(0);

            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    Room currentRoom = Map.MapSquare[i, j];

                    if (i == Player.X && j == Player.Y)
                    {
                        Console.Write("@ ");
                        currentRoom.Visited = true; // Mark the current room as visited
                    }
                    else if (i == Wumpus.X && j == Wumpus.Y)
                    {
                        Console.Write("W ");
                    }
                    else if (currentRoom.Visited)
                    {
                        Console.Write(currentRoom.Content + " ");
                    }
                    else
                    {
                        Console.Write(currentRoom.Content + " ");//Console.Write("? ");
                    }
                }
                Console.WriteLine();
            }
        }

        public void CheckForWumpusSmell()
        {
            // Проверка на наличие запаха Wumpus в текущей комнате.
            wumpusSmell = IsCloseToWumpus(Player.X, Player.Y, Wumpus.X, Wumpus.Y);
            if (wumpusSmell)
            {
                Console.WriteLine("I smell a Wumpus");
            }
        }

        public void CheckForPitWind()
        {
            // Проверка на наличие ветра (яма) в текущей комнате.
            pitWind = IsCloseToPit(Player.X, Player.Y);
            if (pitWind)
            {
                Console.WriteLine("I feel a wind");
            }
        }

        public void CheckForBatsSound()
        {
            // Проверка на наличие скрежита крыльев в соседней  комнате.
            betSound = IsCloseToBat(Player.X, Player.Y);
            if (betSound)
            {
                Console.WriteLine("Bats nearby");
            }
        }

        private bool IsCloseToBat(int x, int y)
        {
            // Проверка на соседство с Мышами.
            return IsBat(x - 1, y) || IsBat(x + 1, y) || IsBat(x, y - 1) || IsBat(x, y + 1);
        }

        private bool IsCloseToWumpus(int playerX, int playerY, int wumpusX, int wumpusY)
        {
            // Проверка на соседство с Wumpus.
            return IsWumpus(playerX, playerY, wumpusX - 1, wumpusY) ||
                   IsWumpus(playerX, playerY, wumpusX + 1, wumpusY) ||
                   IsWumpus(playerX, playerY, wumpusX, wumpusY - 1) ||
                   IsWumpus(playerX, playerY, wumpusX, wumpusY + 1);
        }

        private bool IsCloseToPit(int x, int y)
        {
            // Проверка на соседство с ямами.
            return IsPit(x - 1, y) || IsPit(x + 1, y) || IsPit(x, y - 1) || IsPit(x, y + 1);
        }


        private bool IsWumpus(int playerX, int playerY, int wumpusX, int wumpusY)
        {
            return validSerivice.IsValid(wumpusX, wumpusY, Map.Size) && playerX == wumpusX && playerY == wumpusY;
        }

        private bool IsBat(int x, int y)
        {
            return validSerivice.IsValid(x, y, Map.Size) && Map.MapSquare[x, y].Content == Bat.symbol;
        }

        private bool IsPit(int x, int y)
        {
            return validSerivice.IsValid(x, y, Map.Size) && Map.MapSquare[x, y].Content == Pit.symbol;
        }

        public void ExecuteCommand(ICommand command)
        {
            command.Execute();
            commandHistory.Add(command);
            Wumpus.RandomMoveWumpus(Map);
            Encountered(Player.X, Player.Y, Wumpus.X, Wumpus.Y);
            PrintWorld();
            CheckForWumpusSmell();
            CheckForPitWind();
            CheckForBatsSound();
        }

        public void SetQuantityPits()
        {
            QuantityPits = userInputService.GetValidUserInput("Enter your quantity pits: "); 
        }

        public void SetQuantityTreasures()
        {
            QuantityTreasure = userInputService.GetValidUserInput("Enter your quantity treasures: "); 
        }

        public void SetQuantityBats()
        {
            QuantityBats = userInputService.GetValidUserInput("Enter your quantity bats: "); 
        }

        public void Encountered(int directionX, int directionY, int wumpusX, int wumpusY)
        {
            if (directionX == wumpusX && directionY == wumpusY)
            {
                Console.WriteLine("Game over! Encountered the Wumpus.");
                Environment.Exit(0);
            }
        }
    }
}
