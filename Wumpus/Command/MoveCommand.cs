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
        private Player _player;
        private int _newX, _newY;
        private Map _map;
        private Random _random;

        public MoveCommand(Player player, int newX, int newY, Map map)
        {
            this._player = player;
            this._newX = newX;
            this._newY = newY;
            this._map = map;
            this._random = new Random();
        }   

        public void Execute()
        {
            MovePlayer(_newX, _newY);
        }

        private void MovePlayer(int newX, int newY)
        {
            _player.X = newX; _player.Y = newY;

            var content = _map.MapSquare[_player.X, _player.Y].Content;

            if (content == Pit.symbol)
            {
                Console.WriteLine("Game over! You fell into a pit.");
                Environment.Exit(0);
            }

            if (content == Bat.symbol)
            {
                Console.WriteLine("Encountered the Bat! You've been carried to a new location.");

                int mapSize = _map.Size;

                do
                {
                    newX = _random.Next(mapSize);
                    newY = _random.Next(mapSize);
                }
                while (newX == _player.X && newY == _player.Y);

                _player.X = newX;
                _player.Y = newY;

                GetProblemOnMove();
            }

            if (content == Treasure.symbol)
            {
                Console.WriteLine("Congratulations! You found the treasure and won the game.");
                Environment.Exit(0);
            }
        }

        private void GetProblemOnMove()
        {
            var content = _map.MapSquare[_player.X, _player.Y].Content;

            if (content == Pit.symbol)
            {
                Console.WriteLine("Game over! You fell into a pit.");
                Environment.Exit(0);
            }

            if (content == Treasure.symbol)
            {
                Console.WriteLine("Congratulations! You found the treasure and won the game.");
                Environment.Exit(0);
            }
        }
    }
}
            

