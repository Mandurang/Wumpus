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

        public DirectionVector GetRandomDirection()
        {
            var random = new Random();
            int randomMove = random.Next(1, 5);

            var (x, y) = randomMove switch
            {
                1 => (-1, 0), // Up
                2 => (1, 0),  // Down
                3 => (0, -1), // Left
                4 => (0, 1),  // Right
                _ => (0, 0)   // No movement (default case)
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

