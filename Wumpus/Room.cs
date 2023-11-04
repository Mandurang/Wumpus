using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wumpus
{
    public class Room
    {
        public char Content { get; set; }

        public bool Visited { get; set; } = false;

        public Room(char content)
        {
            Content = content;
        }



        public char IsVisited(bool isVisited) => !isVisited ? '?' : Content;
        
        public bool IsEmpty()
        {
            return Content == '_';
        }

        public override string ToString()
        {
            return Content.ToString();
        }
    }
}
