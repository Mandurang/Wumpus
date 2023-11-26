using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WumpusWorld.MapObject;

namespace WumpusWorld.Command
{
    public class RandomMoveCommand : ICommand
    {
        private Wumpus _wumpus;
        private Map _map;
        private Random Random = new Random();
        private DirectionVector _directionVector = new DirectionVector();

        public RandomMoveCommand(Wumpus wumpus, Map map)
        {
            _wumpus = wumpus;
            _map = map;
        }

        public void Execute()
        {
            RandomMoveWumpus();
        }

        public void RandomMoveWumpus()
        {
            int chanceMoveWumpus = Random.Next(1, 5);

            if (chanceMoveWumpus != 1)
            {
                int newX, newY;

                do
                {
                    DirectionVector directionVector = _directionVector.GetRandomDirection();

                    newX = _wumpus.X + directionVector.X;
                    newY = _wumpus.Y + directionVector.Y;

                } while (!_map.IsValid(newX, newY));

                _wumpus.X = newX;
                _wumpus.Y = newY;
            }
        }
    }
}
