using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wumpus
{
    public class Placer 
    {
        public Player PlacePlayer(Random random, char[][] mapSquare, int worldSize)
        {
            int X, Y;
            do
            {
                X = random.Next(worldSize);
                Y = random.Next(worldSize);
            } while (mapSquare[X][Y] != '_');
            Player player = new Player { X = X, Y = Y};
            return player;
        }

        public List<Wumpus> PlaceWumpuses(int quantityWumpuses, Random random, char[][] mapSquare, int worldSize)
        {
            return PlaceMapObject<Wumpus>(quantityWumpuses, random, mapSquare, worldSize, 'W');
        }

        public List<Pit> PlacePits(int quantityPits, Random random, char[][] mapSquare, int worldSize)
        {
            return PlaceMapObject<Pit>(quantityPits, random, mapSquare, worldSize, 'P');
        }

        public List<Treasure> PlaceTreasures(int quantityTreasures, Random random, char[][] mapSquare, int worldSize)
        {
            return PlaceMapObject<Treasure>(quantityTreasures, random, mapSquare, worldSize, 'G');
        }

        public List<Bat> PlaceBats(int quantityBats, Random random, char[][] mapSquare, int worldSize)
        {
            return PlaceMapObject<Bat>(quantityBats, random, mapSquare, worldSize, 'B');
        }

        private List<T> PlaceMapObject<T>(int quantity, Random random, char[][] mapSquare, int worldSize, char symbol) where T : MapObject, new()
        {
            List<T> mapObjects = new List<T>();

            for (int i = 0; i < quantity; i++)
            {
                int X, Y;

                do
                {
                    X = random.Next(worldSize);
                    Y = random.Next(worldSize);
                } while (mapSquare[X][Y] != '_');
                mapSquare[X][Y] = symbol;
                T newObject = new T { X = X, Y = Y };
                mapObjects.Add(newObject);

            }
            return mapObjects;
        }
    }
}
