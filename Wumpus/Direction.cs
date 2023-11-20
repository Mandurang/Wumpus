using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WumpusWorld
{
    public class DirectionVector
    {
        public readonly int X, Y;

        public DirectionVector(int x, int y)
        {
            X = x;
            Y = y;
        }

        public DirectionVector()
        { }

        public DirectionVector GetDirection(Direction? direction)
        {
            var (x, y) = direction switch
            {
                Direction.Up => (-1, 0),
                Direction.Down => (1, 0),
                Direction.Left => (0, -1),
                Direction.Right => (0, 1),
            };

            return new DirectionVector(x, y);
        }

    }

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
}

