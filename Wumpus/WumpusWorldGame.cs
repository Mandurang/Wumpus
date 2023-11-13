﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wumpus
{
    public class WumpusWorldGame
    {
        public Map Map { get; }
        public int QuantityPits { get; set; }
        public int QuantityTreasure { get; set; }
        public int QuantityWumpus { get; set; }
        public int QuantityBats { get; set; }
        public List<Treasure> Treasures { get; set; }
        public List<Pit> Pits { get; set; }
        public List<Wumpus> Wumpuses { get; set; }
        public List<Bat> Bats { get; set; }
        public Player Player { get; set; }
        public Wumpus Wumpus { get; set; }

        private bool wumpusSmell = false; // Флаг для запаха Wumpus.

        private bool pitWind = false;     // Флаг для драфта (яма).
        private bool betSound = false;     // Флаг для скрежита крильев (bet).

        private Random random = new Random();

        private Placer placer = new Placer();

        private List<ICommand> commandHistory = new List<ICommand>();

        public WumpusWorldGame()
        {
            Map = new Map();
            Player = new Player(0, 0);
        }

        public void GenerateWorld()
        {
            
            Treasures = placer.PlaceTreasures(QuantityTreasure, random, Map.MapSquare);

            Pits = placer.PlacePits(QuantityPits, random, Map.MapSquare);

            Wumpuses = placer.PlaceWumpuses(QuantityWumpus, random, Map.MapSquare);

            Bats = placer.PlaceBats(QuantityBats, random, Map.MapSquare);

            Player = placer.PlacePlayer(random, Map.MapSquare);
        }

        public void PrintWorld()
        {
            Console.Clear();

            for (int i = 0; i < Map.MapSquare.GetLength(0); i++)
            {
                for (int j = 0; j < Map.MapSquare.GetLength(1); j++)
                {
                    if (i == Player.X && j == Player.Y)
                        Console.Write("@ ");
                    Console.Write(Map.MapSquare[i, j].IsVisited() + " ");
                }
                Console.WriteLine();
            }
        }

        public bool IsValid(int x, int y)
        {
            return x >= 0 && x <  Map.MapSquare.GetLength(0) && y >= 0 && y < Map.MapSquare.GetLength(1);
        }

        private bool IsValidMapForWumpus(int x, int y)
        {
            if (IsValid(x, y))
            {
                if (Map.MapSquare[x,y].Content == '_')
                {
                    return x >= 0 && x < Map.MapSquare.GetLength(0) && y >= 0 && y < Map.MapSquare.GetLength(1);
                }
            }
            
            return false;
        }

        public void CheckForWumpusSmell()
        {
            // Проверка на наличие запаха Wumpus в текущей комнате.
            wumpusSmell = IsCloseToWumpus(Player.X, Player.Y);
            if (wumpusSmell)
            {
                Console.WriteLine("I smell a Wumpus");
            }
        }

        public void CheckForPitWind()
        {
            // Проверка на наличие ветра (яма) в текущей комнате.
            pitWind = IsCloseToPit(Player.X, Player.Y);
            if (pitWind)
            {
                Console.WriteLine("I feel a wind");
            }
        }

        public void CheckForBatsSound()
        {
            // Проверка на наличие скрежита крыльев в соседней  комнате.
            betSound = IsCloseToBat(Player.X, Player.Y);
            if (betSound)
            {
                Console.WriteLine("Bats nearby");
            }
        }

        private bool IsCloseToBat(int x, int y)
        {
            // Проверка на соседство с Мышами.
            return IsBat(x - 1, y) || IsBat(x + 1, y) || IsBat(x, y - 1) || IsBat(x, y + 1);
        }

        private bool IsCloseToWumpus(int x, int y)
        {
            // Проверка на соседство с Wumpus.
            return IsWumpus(x - 1, y) || IsWumpus(x + 1, y) || IsWumpus(x, y - 1) || IsWumpus(x, y + 1);
        }

        private bool IsCloseToPit(int x, int y)
        {
            // Проверка на соседство с ямами.
            return IsPit(x - 1, y) || IsPit(x + 1, y) || IsPit(x, y - 1) || IsPit(x, y + 1);
        }

        private bool IsWumpus(int x, int y)
        {
            return IsValid(x, y) && Map.MapSquare[x, y].Content == 'W';
        }

        private bool IsBat(int x, int y)
        {
            return IsValid(x, y) && Map.MapSquare[x, y].Content == 'B';
        }

        private bool IsPit(int x, int y)
        {
            return IsValid(x, y) && Map.MapSquare[x, y].Content == 'P';
        }

        public void ExecuteCommand(ICommand command)
        {
            command.Execute();
            commandHistory.Add(command);
            PrintWorld();
            CheckForWumpusSmell();
            CheckForPitWind();
            CheckForBatsSound();
        }

        public void SetQuantityWumpuses()
        {
            Console.Write("Enter your quantity Wumpuses: ");
            int quantityWumpuses = Int32.Parse(Console.ReadLine());
            QuantityWumpus = quantityWumpuses;
        }

        public void SetQuantityPits()
        {
            Console.Write("Enter your quantity pits: ");
            int quantityPits = Int32.Parse(Console.ReadLine());
            QuantityPits = quantityPits;
        }

        public void SetQuantityTreasures()
        {
            Console.Write("Enter your quantity treasures: ");
            int quantityTreasure = Int32.Parse(Console.ReadLine());
            QuantityPits = quantityTreasure;
        }

        public void SetQuantityBats()
        {
            Console.Write("Enter your quantity bats: ");
            int quantityBats = Int32.Parse(Console.ReadLine());
            QuantityBats = quantityBats;
        }





        //public void SetWorldSize()
        //{
        //    Console.Write("Enter your size world: ");
        //    int size = Int32.Parse(Console.ReadLine());
        //    WorldSize = size;
        //}
    }
}
