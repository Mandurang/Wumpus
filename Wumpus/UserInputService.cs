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
    }
}
