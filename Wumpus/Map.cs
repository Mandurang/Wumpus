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

        public int Width { get; set; }
        public int Height { get; set; }

        public void GenereteMap()
        {
            GetMapSize();
            MapSquare = new Room[Width, Height];
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    MapSquare[i, j] = new Room('_');
                }
            }
        }

        private void GetMapSize()
        {
            UserInputService userInput = new UserInputService();

            Width = userInput.GetValidUserInput("Enter the Width of the map:");
            Height = userInput.GetValidUserInput("Enter the Height of the map:");
        }

        public bool IsValid(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }
    }
}