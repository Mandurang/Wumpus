using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WumpusWorld.MapObject;

namespace WumpusWorld.Command
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
            this.random = new Random();
        }

        public void Execute()
        {
            MovePlayer(newX, newY);
        }

        private void MovePlayer(int newX, int newY)
        {
            player.X = newX; player.Y = newY;

            var content = map.MapSquare[player.X, player.Y].Content;

            if (content == 'P')
            {
                Console.WriteLine("Game over! You fell into a pit.");
                Environment.Exit(0);
            }

            if (content == 'B')
            {
                Console.WriteLine("Encountered the Bat! You've been carried to a new location.");

                int mapSize = map.MapSquare.GetLength(0);

                do
                {
                    newX = random.Next(mapSize);
                    newY = random.Next(mapSize);
                }
                while (newX == player.X && newY == player.Y);

                player.X = newX;
                player.Y = newY;

                GetProblemOnMove();
            }

            if (content == 'T')
            {
                Console.WriteLine("Congratulations! You found the treasure and won the game.");
                Environment.Exit(0);
            }
        }

        private void GetProblemOnMove()
        {
            var content = map.MapSquare[player.X, player.Y].Content;

            if (content == 'P')
            {
                Console.WriteLine("Game over! You fell into a pit.");
                Environment.Exit(0);
            }

            if (content == 'T')
            {
                Console.WriteLine("Congratulations! You found the treasure and won the game.");
                Environment.Exit(0);
            }
        }
    }
}
            

