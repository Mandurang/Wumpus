using System;
using System.Collections.Generic;
using System.IO;
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
        public int QuantityBats { get; set; }
        public List<Treasure> Treasures { get; set; }
        public List<Pit> Pits { get; set; }
        public List<Wumpus> Wumpuses { get; set; }
        public List<Bat> Bats { get; set; }
        public Player Player { get; set; }
        public Wumpus Wumpus { get; set; }

        private bool wumpusSmell = false; // Флаг для запаха Wumpus.

        private bool pitWind = false;     // Флаг для драфта (яма).
        private bool betSound = false;     // Флаг для скрежита крильев (bet).

        private Random random = new Random();

        private Placer placer = new Placer();

        public void GenerateWorld()
        {
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

            Treasures = placer.PlaceTreasures(QuantityTreasure, random, MapSquare, WorldSize);

            Pits = placer.PlacePits(QuantityPits, random, MapSquare, WorldSize);

            Wumpuses = placer.PlaceWumpuses(QuantityWumpus, random, MapSquare, WorldSize);

            Bats = placer.PlaceBats(QuantityBats, random, MapSquare, WorldSize);

            Player = placer.PlacePlayer(random, MapSquare, WorldSize);

        }

        private bool IsValid(int x, int y)
        {
            return x >= 0 && x < WorldSize && y >= 0 && y < WorldSize;
        }

        private bool IsValidMapForWumpus(int x, int y)
        {
            if (IsValid(x, y))
            {
                if (MapSquare[x][y] == '_')
                {
                    return x >= 0 && x < WorldSize && y >= 0 && y < WorldSize;
                }
            }
            
            return false;
        }

        public void CheckForWumpusSmell()
        {
            // Проверка на наличие запаха Wumpus в текущей комнате.
            wumpusSmell = IsCloseToWumpus(Player.X, Player.Y);
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
            // Проверка на соседство с Wumpus.
            return IsBat(x - 1, y) || IsBat(x + 1, y) || IsBat(x, y - 1) || IsBat(x, y + 1);
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

        private bool IsBat(int x, int y)
        {
            return IsValid(x, y) && MapSquare[x][y] == 'B';
        }

        private bool IsPit(int x, int y)
        {
            return IsValid(x, y) && MapSquare[x][y] == 'P';
        }

        public void SetQuantityWumpuses()
        {
            Console.Write("Enter your quantity Wumpuses: ");
            int quantityWumpuses = Int32.Parse(Console.ReadLine());
            QuantityWumpus = quantityWumpuses;
        }

        public void SetQuantityPits()
        {
            Console.Write("Enter your quantity pits: ");
            int quantityPits = Int32.Parse(Console.ReadLine());
            QuantityPits = quantityPits;
        }

        public void SetQuantityTreasures()
        {
            Console.Write("Enter your quantity treasures: ");
            int quantityTreasure = Int32.Parse(Console.ReadLine());
            QuantityPits = quantityTreasure;
        }

        public void SetQuantityBats()
        {
            Console.Write("Enter your quantity bats: ");
            int quantityBats = Int32.Parse(Console.ReadLine());
            QuantityBats = quantityBats;
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
                    if (i == Player.X && j == Player.Y)
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

            Visited[Player.X][Player.Y] = true;
            Player.X = newX;
            Player.Y = newY;

            if (MapSquare[Player.X][Player.Y] == 'P')
            {
                Console.WriteLine("Game over! You fell into a pit.");
                Environment.Exit(0);
            }

            if (MapSquare[Player.X][Player.Y] == 'W')
            {
                Console.WriteLine("Game over! Encountered the Wumpus.");
                Environment.Exit(0);
            }

            if (MapSquare[Player.X][Player.Y] == 'B')
            {
                Console.WriteLine("Go to! Encountered the Bet.");
                Player.X = random.Next(WorldSize);
                Player.Y = random.Next(WorldSize);
            }

            else if (MapSquare[Player.X][Player.Y] == 'G')
            {
                Console.WriteLine("Congratulations! You found the treasure and won the game.");
                Environment.Exit(0);
            }

            PrintWorld();
            CheckForWumpusSmell(); // Проверка запаха Wumpus после перемещения игрока.
            CheckForPitWind();    // Проверка ветра (яма) после перемещения игрока.
            CheckForBatsSound();
        }

        public void ShootArrow(int directionX, int directionY)
        {
            if (Player.QuantityArrow > 0)
            {
                Player.QuantityArrow--;
                Console.WriteLine(" - Direction. You shoot an arrow!");

                int x = Player.X;
                int y = Player.Y;

                for (int i = 0; i < 1; i++)
                {
                    x += directionX;
                    y += directionY;

                    if (IsValid(x, y) && MapSquare[x][y] == 'W')
                    {
                        Console.WriteLine("Congratulations! You shot the Wumpus and won the game.");
                        Environment.Exit(0);
                    }
                }

                Console.WriteLine("You missed. The Wumpus is still alive.");
            }
            else
            {
                Console.WriteLine("Out of arrows!");
            }
        }

        public void RandomMoveWumpus()
        {
            foreach (var wumpus in Wumpuses)
            {
                int newX = wumpus.X;
                int newY = wumpus.Y;
                int chanceMoveWumpus = random.Next(1, 5);

                if (chanceMoveWumpus != 1)
                {
                    do
                    {
                        newX = wumpus.X;
                        newY = wumpus.Y;
                        int randomMove = random.Next(1, 5);
                        switch (randomMove)
                        {
                            case 1:
                                newX = wumpus.X - 1;
                                break;
                            case 2:
                                newY = wumpus.Y - 1;
                                break;
                            case 3:
                                newX = wumpus.X + 1;
                                break;
                            case 4:
                                newY = wumpus.Y + 1;
                                break;
                        }
                    } while (!IsValidMapForWumpus(newX, newY));
                    MapSquare[wumpus.X][wumpus.Y] = '_';
                    wumpus.X = newX;
                    wumpus.Y = newY;
                    MapSquare[wumpus.X][wumpus.Y] = 'W';
                }
            }
        }
    }
}
