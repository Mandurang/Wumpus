using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WumpusWorld
{
    public class UserInputService
    {
        public int GetValidUserInput(string prompt)
        {
            int value = 0;
            bool valid = true;

            do
            {
                Console.WriteLine(prompt);
                var inputNumber = Console.ReadLine();

                if (Int32.TryParse(inputNumber, out value))
                {
                    if (value < 0)
                    {
                        valid = false;
                        Console.WriteLine("Please enter a non-negative number!");
                    }
                    else
                    {
                        valid = true;
                    }
                }
                else
                {
                    Console.WriteLine("Not the number, add number");
                    valid = false;
                }

            } while (!valid);

            return value;
        }

        public Direction? GetValidUserMoveInput(char move)
        {
            Direction? direction;
            bool valid = true;

            do
            {
                //Console.Write("Enter your move (W/A/S/D) or press 'Ctrl' to shoot: ");
                //char move = Console.ReadKey().KeyChar;
                //Console.WriteLine();

                direction = ExecuteDirectionV3(move);

                if (direction is null)
                {
                    Console.WriteLine("Wrong command");
                    valid = false;
                    Console.Write("Enter your move (W/A/S/D) or press 'Alt' to shoot: ");
                    move = Console.ReadKey().KeyChar;
                    Console.WriteLine();
                }
                else
                {
                    valid = true;
                    return direction;
                }

            } while (!valid);

            return direction;
        }

        private Direction? ExecuteDirectionV3(char move)
        {
            return move switch
            {
                'W' => Direction.Up,
                'S' => Direction.Down,
                'A' => Direction.Left,
                'D' => Direction.Right,
                _ => null
            };
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
