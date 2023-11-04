using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wumpus
{
    public class Map
    {
        //public int WorldSize { get; set; }// Размер мира. Можете изменить на нужное значение.
        public Room[,] MapSquare { get; set; }


        //public Map(int size)
        //{
        //    MapSquare = new Room[size, size];
        //    for (int i = 0; i < size; i++)
        //    {
        //        for (int j = 0; j < size; j++)
        //        {
        //            MapSquare[i, j] = new Room('_');
        //        }
        //    }
        //}
        public Map()
        {
            Console.WriteLine("Enter the size of the map:");
            int size = int.Parse(Console.ReadLine());
            MapSquare = new Room[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    MapSquare[i, j] = new Room('_');
                }
            }
        }

        public Room GetTile(int x, int y)
        {
            if (x >= 0 && x < MapSquare.GetLength(0) && y >= 0 && y < MapSquare.GetLength(1))
            {
                return MapSquare[x, y];
            }
            throw  new ArgumentOutOfRangeException("Index X or Y out of range");
        }

        public bool IsValid(int x, int y)
        {
            return x >= 0 && x < MapSquare.GetLength(0) && y >= 0 && y < MapSquare.GetLength(1);
        }

       

    }
}
