using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WumpusWorld.MapObject
{
    public class Placer
    {
        public Wumpus PlaceWumpus(Random random, Room[,] mapSquare)
        {
            int worldSize = mapSquare.GetLength(0);
            int X, Y;
            do
            {
                X = random.Next(worldSize);
                Y = random.Next(worldSize);
            } while (mapSquare[X, Y].Content != '_');
            
            Wumpus wumpus = new Wumpus(X, Y);
            //mapSquare[X, Y].Content = Wumpus.symbol;
            return wumpus;
        }

        public Player PlacePlayer(Random random, Room[,] mapSquare)
        {
            int worldSize = mapSquare.GetLength(0);
            int X, Y;
            do
            {
                X = random.Next(worldSize);
                Y = random.Next(worldSize);
            } while (mapSquare[X, Y].Content != '_');
            Player player = new Player(X, Y);
            return player;
        }

        public List<Pit> PlacePits(int quantityPits, Random random, Room[,] mapSquare)
        {
            return PlaceMapObject<Pit>(quantityPits, random, mapSquare, Pit.symbol);
        }

        public List<Treasure> PlaceTreasures(int quantityTreasures, Random random, Room[,] mapSquare)
        {
            return PlaceMapObject<Treasure>(quantityTreasures, random, mapSquare, Treasure.symbol);
        }

        public List<Bat> PlaceBats(int quantityBats, Random random, Room[,] mapSquare)
        {
            return PlaceMapObject<Bat>(quantityBats, random, mapSquare, Bat.symbol);
        }

        private List<T> PlaceMapObject<T>(int quantity, Random random, Room[,] mapSquare, char symbol) where T : MapObject, new()
        {
            List<T> mapObjects = new List<T>();

            int worldSize = mapSquare.GetLength(0);

            for (int i = 0; i < quantity; i++)
            {
                int X, Y;

                do
                {
                    X = random.Next(worldSize);
                    Y = random.Next(worldSize);
                } while (mapSquare[X, Y].Content != '_');
                mapSquare[X, Y].Content = symbol;
                T newObject = new T { X = X, Y = Y };
                mapObjects.Add(newObject);

            }
            return mapObjects;
        }
    }
}

//public List<Wumpus> PlaceWumpuses(int quantityWumpuses, Random random, Room[,] mapSquare)
//{
//    return PlaceMapObject<Wumpus>(quantityWumpuses, random, mapSquare,  Wumpus.symbol);
//}
