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

        public bool Visited { get; set; } = false;

        public Room(char content)
        {
            Content = content;
        }
        public Room() { }
    }
}
