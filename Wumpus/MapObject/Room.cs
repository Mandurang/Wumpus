using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WumpusWorld.MapObject
{
    public class Room : MapObject
    {
        public char Content { get; set; }

        private bool Visited { get; set; }

        public const char symbol = '_';

        public Room(char content)
        {
            Content = content;
        }
        public Room() { }

        public bool IsVisited()
        {
            return Visited = true;
        }

        public bool CheckVisit()
        {
            return Visited;
        }
    }
}
