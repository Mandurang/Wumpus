using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wumpus
{
    public class MapObject
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class Placer 
    {
        public Player PlacePlayer(Random random, char[][] MapSquare, int worldSize)
        {
            int X, Y;
            do
            {
                X = random.Next(worldSize);
                Y = random.Next(worldSize);
            } while (MapSquare[X][Y] != '_');
            Player player = new Player { PlayerX = X, PlayerY = Y};
            return player;
        }

        public List<Wumpus> PlaceWumpus(int quantityWumpus, Random random, char[][] MapSquare, int worldSize)
        {
            return PlaceMapObject<Wumpus>(quantityWumpus, random, MapSquare, worldSize, 'W');
        }
        public List<Pit> PlacePit(int quantityPit, Random random, char[][] MapSquare, int worldSize)
        {
            return PlaceMapObject<Pit>(quantityPit, random, MapSquare, worldSize, 'P');
        }
        public List<Treasure> PlaceTreasure(int quantityTreasure, Random random, char[][] MapSquare, int worldSize)
        {
            return PlaceMapObject<Treasure>(quantityTreasure, random, MapSquare, worldSize, 'G');
        }


        private List<T> PlaceMapObject<T>(int quantity, Random random, char[][] MapSquare, int worldSize, char symbol) where T : MapObject, new()
        {
            List<T> mapObjects = new List<T>();

            for (int i = 0; i < quantity; i++)
            {
                int X, Y;

                do
                {
                    X = random.Next(worldSize);
                    Y = random.Next(worldSize);
                } while (MapSquare[X][Y] != '_');
                MapSquare[X][Y] = symbol;
                T newObject = new T { X = X, Y = Y };
                mapObjects.Add(newObject);

            }
            return mapObjects;
        }
    }
}
