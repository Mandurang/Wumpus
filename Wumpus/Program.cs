using System;

class WumpusWorldGame
{
    static char[][] world = new char[4][]
    {
        new char[] { '_', '_', '_', '_' },
        new char[] { '_', '_', '_', '_' },
        new char[] { '_', '_', '_', '_' },
        new char[] { '_', '_', '_', '_' }
    };

    static bool[][] visited = new bool[4][]
    {
        new bool[] { false, false, false, false },
        new bool[] { false, false, false, false },
        new bool[] { false, false, false, false },
        new bool[] { false, false, false, false }
    };

    static int playerX = 0;
    static int playerY = 0;

    static int wumpusX;
    static int wumpusY;

    static bool wumpusSmell = false; // Флаг для запаха Wumpus.
    static bool pitDraft = false;     // Флаг для драфта (яма).

    static Random random = new Random();

    static void GenerateWorld()
    {
        // Генерация случайного расположения сокровища и ловушек.
        int treasureX = random.Next(4);
        int treasureY = random.Next(4);

        int pit1X = random.Next(4);
        int pit1Y = random.Next(4);

        int pit2X = random.Next(4);
        int pit2Y = random.Next(4);

        while ((treasureX == pit1X && treasureY == pit1Y) ||
               (treasureX == pit2X && treasureY == pit2Y) ||
               (pit1X == pit2X && pit1Y == pit2Y))
        {
            treasureX = random.Next(4);
            treasureY = random.Next(4);

            pit1X = random.Next(4);
            pit1Y = random.Next(4);

            pit2X = random.Next(4);
            pit2Y = random.Next(4);
        }

        world[treasureX][treasureY] = 'G';
        world[pit1X][pit1Y] = 'P';
        world[pit2X][pit2Y] = 'P';

        // Генерация случайного расположения Wumpus.
        wumpusX = random.Next(4);
        wumpusY = random.Next(4);

        while ((wumpusX == treasureX && wumpusY == treasureY) ||
               (wumpusX == pit1X && wumpusY == pit1Y) ||
               (wumpusX == pit2X && wumpusY == pit2Y))
        {
            wumpusX = random.Next(4);
            wumpusY = random.Next(4);
        }

        world[wumpusX][wumpusY] = 'W';
    }

    static bool IsValid(int x, int y)
    {
        return x >= 0 && x < 4 && y >= 0 && y < 4;
    }

    static void CheckForWumpusSmell()
    {
        // Проверка на наличие запаха Wumpus в текущей комнате.
        wumpusSmell = IsAdjacentToWumpus(playerX, playerY);
        if (wumpusSmell)
        {
            Console.WriteLine("I smell a Wumpus");
        }
    }

    static void CheckForPitDraft()
    {
        // Проверка на наличие драфта (яма) в текущей комнате.
        pitDraft = IsAdjacentToPit(playerX, playerY);
        if (pitDraft)
        {
            Console.WriteLine("I feel a draft");
        }
    }

    static bool IsAdjacentToWumpus(int x, int y)
    {
        // Проверка на соседство с Wumpus.
        return (Math.Abs(x - wumpusX) == 1 && y == wumpusY) || (x == wumpusX && Math.Abs(y - wumpusY) == 1);
    }

    static bool IsAdjacentToPit(int x, int y)
    {
        // Проверка на соседство с ямами.
        return IsPit(x - 1, y) || IsPit(x + 1, y) || IsPit(x, y - 1) || IsPit(x, y + 1);
    }

    static bool IsPit(int x, int y)
    {
        return IsValid(x, y) && world[x][y] == 'P';
    }

    static void PrintWorld()
    {
        Console.Clear(); // Очистить консоль перед выводом нового состояния мира.

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (i == playerX && j == playerY)
                    Console.Write("P ");
                else if (visited[i][j])
                    Console.Write(world[i][j] + " ");
                else
                    Console.Write("? ");
            }
            Console.WriteLine();
        }
    }

    static void MovePlayer(int newX, int newY)
    {
        if (!IsValid(newX, newY) || visited[newX][newY])
        {
            Console.WriteLine("Invalid move.");
            return;
        }

        visited[playerX][playerY] = true;
        playerX = newX;
        playerY = newY;

        if (world[playerX][playerY] == 'P' || world[playerX][playerY] == 'W')
        {
            Console.WriteLine("Game over! You fell into a pit or encountered the Wumpus.");
            Environment.Exit(0);
        }
        else if (world[playerX][playerY] == 'G')
        {
            Console.WriteLine("Congratulations! You found the treasure and won the game.");
            Environment.Exit(0);
        }

        PrintWorld();
        CheckForWumpusSmell(); // Проверка запаха Wumpus после перемещения игрока.
        CheckForPitDraft();    // Проверка драфта (яма) после перемещения игрока.
    }

    static void Main(string[] args)
    {
        
        GenerateWorld(); // Генерация случайного мира.
        PrintWorld();
        CheckForWumpusSmell(); // Проверка запаха Wumpus при старте игры.
        CheckForPitDraft();    // Проверка драфта (яма) при старте игры.

        while (true)
        {
            Console.Write("Enter your move (W/A/S/D): ");
            char move = Console.ReadKey().KeyChar;
            Console.WriteLine();

            int newX = playerX;
            int newY = playerY;

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

            MovePlayer(newX, newY);
        }
    }

    
}






