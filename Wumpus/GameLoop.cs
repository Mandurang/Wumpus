using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wumpus
{
    public class GameLoop
    {
        public char[][] world = new char[4][]
        {
        new char[] { '_', '_', '_', '_' },
        new char[] { '_', '_', '_', '_' },
        new char[] { '_', '_', '_', '_' },
        new char[] { '_', '_', '_', '_' }
        };

        public char[][] CreateWorld()
        {
            return world;
        }

        public bool[][] visited = new bool[4][]
        {
        new bool[] { false, false, false, false },
        new bool[] { false, false, false, false },
        new bool[] { false, false, false, false },
        new bool[] { false, false, false, false }
        };

        public bool[][] isVisitedWorld()
        {
            return visited;
        }

        public int playerX = 0, playerY = 0;

        Random random = new Random();

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
            world[playerX][playerY] = '@';
            world[pit1X][pit1Y] = 'P';
            world[pit2X][pit2Y] = 'P';
        }

        public void PrintWorld()
        {
            Console.Clear();

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (i == playerX && j == playerY)
                        Console.Write("@ ");
                    else if (visited[i][j])
                        Console.Write(world[i][j] + " ");
                    else
                        Console.Write("? ");
                }
                Console.WriteLine();
            }
        }
    }
}
