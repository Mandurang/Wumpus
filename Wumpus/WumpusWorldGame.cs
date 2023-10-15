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
        public List<Treasure> Treasures { get; set; }
        public List<Pit> Pits { get; set; }

        public int playerX = 0;
        public int playerY = 0;

        public int wumpusX, wumpusY;

        private bool wumpusSmell = false; // Флаг для запаха Wumpus.
        private bool pitWind = false;     // Флаг для драфта (яма).

        private Random random = new Random();

        public void GenerateWorld()
        {
            Pit pit = new Pit();
            Treasure treasure = new Treasure();

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

            Treasures = treasure.PlaceTreasure(QuantityTreasure, random, MapSquare, WorldSize);

            Pits = pit.PlacePits(QuantityPits, random, MapSquare, WorldSize);

            do
            {
                wumpusX = random.Next(WorldSize);
                wumpusY = random.Next(WorldSize);
            } while (MapSquare[wumpusX][wumpusY] != '_');
            MapSquare[wumpusX][wumpusY] = 'W';

        }

        private bool IsValid(int x, int y)
        {
            return x >= 0 && x < WorldSize && y >= 0 && y < WorldSize;
        }

        public void CheckForWumpusSmell()
        {
            // Проверка на наличие запаха Wumpus в текущей комнате.
            wumpusSmell = IsAdjacentToWumpus(playerX, playerY);
            if (wumpusSmell)
            {
                Console.WriteLine("I smell a Wumpus");
            }
        }

        public void CheckForPitWind()
        {
            // Проверка на наличие ветра (яма) в текущей комнате.
            pitWind = IsAdjacentToPit(playerX, playerY);
            if (pitWind)
            {
                Console.WriteLine("I feel a wind");
            }
        }

        private bool IsAdjacentToWumpus(int x, int y)
        {
            // Проверка на соседство с Wumpus.
            return (Math.Abs(x - wumpusX) == 1 && y == wumpusY) || (x == wumpusX && Math.Abs(y - wumpusY) == 1);
        }

        private bool IsAdjacentToPit(int x, int y)
        {
            // Проверка на соседство с ямами.
            return IsPit(x - 1, y) || IsPit(x + 1, y) || IsPit(x, y - 1) || IsPit(x, y + 1);
        }

        private bool IsPit(int x, int y)
        {
            return IsValid(x, y) && MapSquare[x][y] == 'P';
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
                    if (i == playerX && j == playerY)
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
            if (!IsValid(newX, newY) || Visited[newX][newY])
            {
                Console.WriteLine("Invalid move.");
                return;
            }

            Visited[playerX][playerY] = true;
            playerX = newX;
            playerY = newY;

            if (MapSquare[playerX][playerY] == 'P')
            {
                Console.WriteLine("Game over! You fell into a pit.");
                Environment.Exit(0);
            }

            if (MapSquare[playerX][playerY] == 'W')
            {
                Console.WriteLine("Game over! Encountered the Wumpus.");
                Environment.Exit(0);
            }

            else if (MapSquare[playerX][playerY] == 'G')
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
