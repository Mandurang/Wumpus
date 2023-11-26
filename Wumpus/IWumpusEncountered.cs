using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WumpusWorld.MapObject;

namespace WumpusWorld
{
    public interface IWumpusEncountered
    {
        void Encountered(int directionX, int directionY, int wumpusX, int wumpusY);
    }
}
