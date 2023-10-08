using Wumpus;

public class Program
{
    public static void Main(string[] args)
    {
        GameLoop gameLoop = new GameLoop();
        Move myMove = new Move();
        Console.WriteLine("Welcome to Wumpus World!");
        Console.WriteLine("Legend: ? - Unexplored, _ - Empty, @ - Player, P - Pit, W - Wumpus");
        
        gameLoop.PrintWorld();

        while (true)
        {
            Console.Write("Enter your move (W/A/S/D): ");
            char move = Console.ReadKey().KeyChar;
            Console.WriteLine();

            int newX = gameLoop.playerX;
            int newY = gameLoop.playerY;

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

            myMove.MovePlayer(newX, newY);
        }
    }
}