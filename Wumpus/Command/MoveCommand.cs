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
        interface ICheckResult
        {

        }

        private Player _player;
        private Map _map;
        private Random _random;
        private Direction? _direction;
        private DirectionVector _directionVector = new DirectionVector();
        record GameFinished(string Message) : ICheckResult;
        record PlayerMoved(int X, int Y) : ICheckResult;
        record Nothing() : ICheckResult;

        public MoveCommand(Player player, Direction? direction, Map map)
        {
            _player = player;
            _map = map;
            _random = new Random();
            _direction = direction;
        }


        public void Execute()
        {
            MovePlayer();
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

        private void MovePlayer()
        {
            if(_direction is not null)
            {
                DirectionVector directionVector = _directionVector.GetDirection(_direction);

                int newX = _player.X + directionVector.X;
                int newY = _player.Y + directionVector.Y;

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

        enum ContentOnMove
        {
            FacedWithPit,
            FacedWithTreasure,
            FacedWithBat,
            FacedWithRoom
        }
    } 
}


