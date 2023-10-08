using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wumpus
{
    internal class User
    {
        public int Room { get; set; }
        public bool IsAliveUser { get; set; }
        public int Life { get; set; }
        public bool HasArrow { get ; set; }
        public int Arrow { get; set; } 
    }
}
