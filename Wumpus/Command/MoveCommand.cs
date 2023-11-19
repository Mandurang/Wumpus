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
        private Direction? _direction;

        public MoveCommand(Player player, int newX, int newY, Map map)
        {
            _player = player;
            _newX = newX;
            _newY = newY;
            _map = map;
            _random = new Random();
        }

        public MoveCommand(Player player, Direction? direction, Map map)
        {
            _player = player;
            _map = map;
            _random = new Random();
            _direction = direction;
        }

        public void Execute()
        {
            MovePlayerV2();
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

                GetProblemOnMoveV2(newX, newY);
            }

            if (content == Treasure.symbol)
            {
                Console.WriteLine("Congratulations! You found the treasure and won the game.");
                Environment.Exit(0);
            }
        }

        private void GetProblemOnMoveV2(int newX, int newY)
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
            }
        }

        enum ContentOnMove
        {
            FacedWithPit,
            FacedWithTreasure,
            FacedWithBat,
            FacedWithRoom
        }

        private ContentOnMove GetContentOnMoveV2(char content)
        {
            return content switch
            {
                Pit.symbol => ContentOnMove.FacedWithPit,
                Treasure.symbol => ContentOnMove.FacedWithTreasure,
                Bat.symbol => ContentOnMove.FacedWithBat,
                Room.symbol => ContentOnMove.FacedWithRoom
            };
        }

        private void MovePlayerV2()
        {
            if(_direction is not null)
            {
                var (dx, dy) = _direction switch
                {
                    Direction.Up => (-1, 0),
                    Direction.Down => (1, 0),
                    Direction.Left => (0, -1),
                    Direction.Right => (0, 1),
                };

                int newX = _player.X + dx;
                int newY = _player.Y + dy;

                if (!_map.IsValid(newX, newY))
                {
                    Console.WriteLine("Invalid move. You can't go outside the map.");
                    return;
                }

                _player.X = newX;
                _player.Y = newY;

                CheckContent();
            }
            else
            {
                Console.WriteLine("Invalid move. You can't go outside the map.");
                return;
            }


            //if (!validSerivice.IsValid(newX, newY, wumpusWorld.Map.Size))
            //{
            //    Console.WriteLine("Invalid move. You can't go outside the map.");
            //    continue;
            //}

           
        }

        private ICheckResult CheckContent()
        {
            var result = CheckContentIter();

            if (result is GameFinished gameFinished)
            {
                Console.WriteLine(gameFinished.Message);
                Environment.Exit(0);
            }

            if (result is PlayerMoved playerMoved)
            {
                _player.X = playerMoved.X; _player.Y = playerMoved.Y;

                return CheckContent();
            }

            return new Nothing();
        }

        private ICheckResult CheckContentIter()
        {
            var content = _map.MapSquare[_player.X, _player.Y].Content;

            var problem = GetContentOnMoveV2(content);

            if (problem == ContentOnMove.FacedWithPit)
            {
                return new GameFinished("Game over! You fell into a pit.");
            }

            if (problem == ContentOnMove.FacedWithTreasure)
            {
                return new GameFinished("Congratulations! You found the treasure and won the game.");
            }

            if (problem == ContentOnMove.FacedWithBat)
            {
                Console.WriteLine("Encountered the Bat! You've been carried to a new location.");

                int mapSize = _map.Size;
                int newX, newY;

                do
                {
                    newX = _random.Next(mapSize);
                    newY = _random.Next(mapSize);
                }
                while (newX == _player.X && newY == _player.Y);

                return new PlayerMoved(newX, newY);
            }

            return new Nothing();
        }

        interface ICheckResult
        {
            
        }

        record GameFinished(string Message) : ICheckResult;
        record PlayerMoved(int X, int Y) : ICheckResult;
        record Nothing() : ICheckResult;

    } 
}


