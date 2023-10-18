using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wumpus
{
    public class WumpusWorldGame
    {
        public int WorldSize { get; set; }// Размер мира. Можете изменить на нужное значение.
        public char[][] MapSquare { get; set; }
        public bool[][] Visited { get; set; }
        public int QuantityPits { get; set; }
        public int QuantityTreasure { get; set; }
        public int QuantityWumpus { get; set; }
        public List<Treasure> Treasures { get; set; }
        public List<Pit> Pits { get; set; }
        public List<Wumpus> Wumpuses { get; set; }

        public Player Player { get; set; }

        //public int wumpusX, wumpusY;

        private bool wumpusSmell = false; // Флаг для запаха Wumpus.
        private bool pitWind = false;     // Флаг для драфта (яма).

        private Random random = new Random();

        public void GenerateWorld()
        {
            Placer placer = new Placer();

            // Инициализация мира с заданным размером.
            MapSquare = new char[WorldSize][];
            Visited = new bool[WorldSize][];
            for (int i = 0; i < WorldSize; i++)
            {
                MapSquare[i] = new char[WorldSize];
                Visited[i] = new bool[WorldSize];
                for (int j = 0; j < WorldSize; j++)
                {
                    MapSquare[i][j] = '_';
                    Visited[i][j] = false;
                }
            }

            Treasures = placer.PlaceTreasure(QuantityTreasure, random, MapSquare, WorldSize);

            Pits = placer.PlacePit(QuantityPits, random, MapSquare, WorldSize);

            Wumpuses = placer.PlaceWumpus(QuantityWumpus, random, MapSquare, WorldSize);

            Player = placer.PlacePlayer(random, MapSquare, WorldSize);

        }

        private bool IsValid(int x, int y)
        {
            return x >= 0 && x < WorldSize && y >= 0 && y < WorldSize;
        }

        public void CheckForWumpusSmell()
        {
            // Проверка на наличие запаха Wumpus в текущей комнате.
            wumpusSmell = IsCloseToWumpus(Player.PlayerX, Player.PlayerY);
            if (wumpusSmell)
            {
                Console.WriteLine("I smell a Wumpus");
            }
        }

        public void CheckForPitWind()
        {
            // Проверка на наличие ветра (яма) в текущей комнате.
            pitWind = IsCloseToPit(Player.PlayerX, Player.PlayerY);
            if (pitWind)
            {
                Console.WriteLine("I feel a wind");
            }
        }

        private bool IsCloseToWumpus(int x, int y)
        {
            // Проверка на соседство с Wumpus.
            return IsWumpus(x - 1, y) || IsWumpus(x + 1, y) || IsWumpus(x, y - 1) || IsWumpus(x, y + 1);
        }

        private bool IsCloseToPit(int x, int y)
        {
            // Проверка на соседство с ямами.
            return IsPit(x - 1, y) || IsPit(x + 1, y) || IsPit(x, y - 1) || IsPit(x, y + 1);
        }

        private bool IsWumpus(int x, int y)
        {
            return IsValid(x, y) && MapSquare[x][y] == 'W';
        }

        private bool IsPit(int x, int y)
        {
            return IsValid(x, y) && MapSquare[x][y] == 'P';
        }

        public void SetQuantityWupus()
        {
            Console.Write("Enter your quantity Wumpus: ");
            int quantityWupus = Int32.Parse(Console.ReadLine());
            QuantityWumpus = quantityWupus;
        }

        public void SetQuantityPits()
        {
            Console.Write("Enter your quantity pits: ");
            int quantityPits = Int32.Parse(Console.ReadLine());
            QuantityPits = quantityPits;
        }

        public void SetQuantityTreasure()
        {
            Console.Write("Enter your quantity treasure: ");
            int quantityTreasure = Int32.Parse(Console.ReadLine());
            QuantityPits = quantityTreasure;
        }


        public void SetWorldSize()
        {
            Console.Write("Enter your size world: ");
            int size = Int32.Parse(Console.ReadLine());
            WorldSize = size;
        }

        public void PrintWorld()
        {
            Console.Clear();

            for (int i = 0; i < WorldSize; i++)
            {
                for (int j = 0; j < WorldSize; j++)
                {
                    if (i == Player.PlayerX && j == Player.PlayerY)
                        Console.Write("@ ");
                    else if (Visited[i][j])
                        Console.Write(MapSquare[i][j] + " ");
                    else
                        Console.Write("? ");
                }
                Console.WriteLine();
            }
        }

        public void MovePlayer(int newX, int newY)
        {
            if (!IsValid(newX, newY))
            {
                Console.WriteLine("Invalid move.");
                return;
            }

            Visited[Player.PlayerX][Player.PlayerY] = true;
            Player.PlayerX = newX;
            Player.PlayerY = newY;

            if (MapSquare[Player.PlayerX][Player.PlayerY] == 'P')
            {
                Console.WriteLine("Game over! You fell into a pit.");
                Environment.Exit(0);
            }

            if (MapSquare[Player.PlayerX][Player.PlayerY] == 'W')
            {
                Console.WriteLine("Game over! Encountered the Wumpus.");
                Environment.Exit(0);
            }

            else if (MapSquare[Player.PlayerX][Player.PlayerY] == 'G')
            {
                Console.WriteLine("Congratulations! You found the treasure and won the game.");
                Environment.Exit(0);
            }

            PrintWorld();
            CheckForWumpusSmell(); // Проверка запаха Wumpus после перемещения игрока.
            CheckForPitWind();    // Проверка ветра (яма) после перемещения игрока.
        }
    }
}
