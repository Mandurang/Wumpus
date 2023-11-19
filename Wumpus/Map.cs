using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WumpusWorld.MapObject;

namespace WumpusWorld
{
    public class Map
    {
        public Room[,] MapSquare { get; set; }

        public int Size { get; set; }

        public void GenereteMap()
        {
            GetMapSize();
            MapSquare = new Room[Size, Size];
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    MapSquare[i, j] = new Room('_');
                }
            }
        }

        private void GetMapSize()
        {
            UserInputService userInput = new UserInputService();

            Size = userInput.GetValidUserInput("Enter the size of the map:");
        }

        public bool IsValid(int x, int y)
        {
            return x >= 0 && x < Size && y >= 0 && y < Size;
        }

        //public Room GetRoom(int x, int y)
        //{
        //    if (IsValid(x, y))
        //    {
        //        return MapSquare[x, y];
        //    }
        //    return null;
        //}
    }
}