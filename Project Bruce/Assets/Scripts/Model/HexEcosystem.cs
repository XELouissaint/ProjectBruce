using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bruce {
    public class HexEcosystem
    {
        public HexEcosystem(Hex hex)
        {
            this.hex = hex;
            Trees = new Dictionary<TreeBreed, int>();
            
        }
        Hex hex;
        public Dictionary<TreeBreed, int> Trees;
        public Grass Grass;
        public int space = 100;

        public int TreeTotal
        {
            get
            {
                return Trees.Values.Sum();
            }
        }


        public void GenerateRandomEcosystem()
        {
            GenerateRandomTrees();
        }

        

        void GenerateRandomTrees()
        {
            int rand = 0;

            if(hex.Terrain.WaterSource == WaterSource.Lake)
            {
                return;
            }

            switch (hex.Terrain.SoilType) 
            {
                case SoilType.Clay:
                    rand = World.RNG.Next(5, 20);
                    break;
                case SoilType.Sand:
                    rand = World.RNG.Next(0, 10);
                    break;
                case SoilType.Loam:
                    rand = World.RNG.Next(10, 40);
                    break;
            }

            AddTree(TreeBreed.Oak, rand);

            switch (hex.Terrain.SoilType)
            {
                case SoilType.Clay:
                    rand = World.RNG.Next(5, 20);
                    break;
                case SoilType.Sand:
                    rand = World.RNG.Next(0, 10);
                    break;
                case SoilType.Loam:
                    rand = World.RNG.Next(10, 40);
                    break;
            }

            AddTree(TreeBreed.Apple, rand);

        }

        bool AddTree(TreeBreed breed, int amount)
        {
            if(TreeTotal >= space)
            {
                return false;
            }
            else if (TreeTotal + amount > space)
            {
                amount = space - TreeTotal;

                Trees.Add(breed, amount);
                return true;
            }
            else
            {
                Trees.Add(breed, amount);
                return true;
            }
        }

    }

    
}