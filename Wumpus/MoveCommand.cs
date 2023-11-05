using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wumpus
{
    public class MoveCommand : ICommand
    {
        private Player player;
        private int newX, newY;
        private Map map;
        private Random random;

        public MoveCommand(Player player, int newX, int newY, Map map)
        {
            this.player = player;
            this.newX = newX;
            this.newY = newY;
            this.map = map;
        }

        public void Execute()
        {
            MovePlayer(newX, newY);
        }

        private void MovePlayer(int newX, int newY)
        {
            player.X = newX; player.Y = newY;
            
            if (!map.IsValid(newX, newY))
            {
                Console.WriteLine("Invalid move.");
                return;
            }

            if (map.MapSquare[player.X, player.Y].Content == 'P')
            {
                Console.WriteLine("Game over! You fell into a pit.");
                Environment.Exit(0);
            }

            if (map.MapSquare[player.X, player.Y].Content == 'W')
            {
                Console.WriteLine("Game over! Encountered the Wumpus.");
                Environment.Exit(0);
            }

            if (map.MapSquare[player.X, player.Y].Content == 'B')
            {
                Console.WriteLine("Go to! Encountered the Bet.");
                player.X = random.Next(map.MapSquare.GetLength(0));
                player.Y = random.Next(map.MapSquare.GetLength(1));
            }

            else if (map.MapSquare[player.X, player.Y].Content == 'G')
            {
                Console.WriteLine("Congratulations! You found the treasure and won the game.");
                Environment.Exit(0);
            }
        }
    }
}
