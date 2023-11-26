using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WumpusWorld
{
    public class ValidService
    {
        public bool IsValid(int x, int y, int size)
        {
            return x >= 0 && x < size && y >= 0 && y < size;
        }
    }
}
