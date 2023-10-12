using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wumpus
{
    public class World
    {
        public char[][] MapSquare { get; set; }
        public int WorldSize { get; set; }
        public int QuantityPit { get; set; }
        public int QuantityWumpus { get; set; }
        public bool[][] Visited { get; set; }

        public int playerX = 0, playerY = 0;

        Random random = new Random();

        public World(char[][] mapSquare, int worldSize, int quantityPit, int quantityWumpus, bool[][] visited)
        {
            MapSquare = mapSquare;
            WorldSize = worldSize;
            QuantityPit = quantityPit;
            QuantityWumpus = quantityWumpus;
            Visited = visited;
        }

        public void SetWorldSize()
        {
            var userInput = Console.ReadLine();
            if (Int32.TryParse(userInput, out int worldSize))
            {
                Console.WriteLine("Your world is {0}", userInput);
                WorldSize = worldSize;
            }
            else
            {
                Console.WriteLine("Wrong number, please add number to continue");
            }
        }

        public void CallWorldMenu()
        {
            Console.WriteLine("Welcome to Wumpus World!");
            Console.WriteLine("Add size your world: ");
            SetWorldSize();
            Console.WriteLine("Legend: ? - Unexplored, _ - Empty, P - Player, P - Pit, W - Wumpus, G - Gold");
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
                    else if (visited[i][j])
                        Console.Write(MapSquare[i][j] + " ");
                    else
                        Console.Write("? ");
                }
                Console.WriteLine();
            }
        }

        public void CreateWorld()
        {
            MapSquare = new char[WorldSize][];
            Visited = new bool[WorldSize][];
            for (int i = 0; i < WorldSize; i++)
            {
                for (int j = 0; j < WorldSize; j++)
                {
                    MapSquare[i][j] = '_';
                    Visited[i][j] = false;
                }
            }
        }

        public void GenerateWorld()
        {
            int wumpusX = random.Next(4);
            int wumpusY = random.Next(4);

            int pit1X = random.Next(4);
            int pit1Y = random.Next(4);

            int pit2X = random.Next(4);
            int pit2Y = random.Next(4);

            while ((wumpusX == pit1X && wumpusY == pit1Y) ||
                   (wumpusX == pit2X && wumpusY == pit2Y) ||
                   (pit1X == pit2X && pit1Y == pit2Y))
            {
                wumpusX = random.Next(4);
                wumpusY = random.Next(4);

                pit1X = random.Next(4);
                pit1Y = random.Next(4);

                pit2X = random.Next(4);
                pit2Y = random.Next(4);
            }

            world[wumpusX][wumpusY] = 'W';
            //world[playerX][playerY] = '@';
            world[pit1X][pit1Y] = 'P';
            world[pit2X][pit2Y] = 'P';
        }


        public bool[][] isVisitedWorld()
        {
            
        }

        /*
         *     world = new char[worldSize][];
        visited = new bool[worldSize][];
        for (int i = 0; i < worldSize; i++)
        {
            world[i] = new char[worldSize];
            visited[i] = new bool[worldSize];
            for (int j = 0; j < worldSize; j++)
            {
                world[i][j] = '_';
                visited[i][j] = false;
            }
        }
         */

        //public char[][] world = new char[4][]
        //{
        //new char[] { '_', '_', '_', '_' },
        //new char[] { '_', '_', '_', '_' },
        //new char[] { '_', '_', '_', '_' },
        //new char[] { '_', '_', '_', '_' }
        //};

        //public bool[][] visited = new bool[4][]
        //{
        //new bool[] { false, false, false, false },
        //new bool[] { false, false, false, false },
        //new bool[] { false, false, false, false },
        //new bool[] { false, false, false, false }
        //};



        
    }
}
