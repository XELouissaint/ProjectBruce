using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bruce
{
    public class World
    {
        public World(Map map = null)
        {
            if(map == null)
            {
                Map = new Map();
            }
            else
            {
                Map = map;
            }

            Countries = new HashSet<Country>();
            ActiveCountries = new HashSet<Country>();

            Map.GenerateHexGrid();
            SpawnStartCountries();
        }

        public static System.Random RNG = new System.Random();


        public Map Map;

        public Country Nature;
        public HashSet<Country> Countries;
        public HashSet<Country> ActiveCountries;

        public void Tick()
        {
            foreach (Country country in ActiveCountries)
            {
                country.Tick();
            }
        }

        public void SpawnStartCountries()
        {
            Nature = new Country();
            Nature.Name = "Nature";
            for (int i = 0; i < 1; i++)
            {
                Country country = new Country();
                country.Name = "Country " + i;

                Hex hex = GetOptimalHexForSettlement();

                Settlement settlement = new Settlement(country, hex);
                settlement.Population.GenerateRandomPopulation(10);

                country.AddSettlement(settlement);
                country.RefreshPopulation();
                settlement.RefreshPopulation();

                Countries.Add(country);
                ActiveCountries.Add(country);
                
            }
        }

        public void SpawnStartAnimals()
        {
            Animal animal = new Animal(AnimalFactory.SheepBreed);

            AnimalUnit animalUnit = new AnimalUnit(Nature, RecursivelyGetFreeHex());
            animalUnit.Animal = animal;

            Nature.UnitManager.AddUnit(animalUnit);
        }

        public Hex RecursivelyGetFreeHex()
        {
            int randX = RNG.Next(0, Map.Width);
            int randZ = RNG.Next(0, Map.Height);

            Hex hex = Map.HexGrid[randX, randZ];

            if (hex.Owner != null)
            {
                hex = RecursivelyGetFreeHex();
            }

            if (hex.Terrain.WaterSource == WaterSource.Lake) 
            {
                RecursivelyGetFreeHex();
            }

            return hex;
        }

        public Hex GetOptimalHexForSettlement()
        {
            Dictionary<Hex, int> hexScores = new Dictionary<Hex, int>();

            Hex selected = null;

            foreach (Hex hex in Map.HexGrid)
            {
                hexScores[hex] = 0;
                if(hex.Terrain.WaterSource == WaterSource.Lake)
                {
                    continue;
                }

                if(hex.Owner != null)
                {
                    continue;
                }

                if(hex.Terrain.SoilType == SoilType.Clay)
                {
                    hexScores[hex] += 10;
                }

                foreach (Hex neighbor in hex.Neighbors())
                {
                    if (neighbor.Terrain.WaterSource == WaterSource.Lake)
                    {
                        hexScores[hex] += 10;
                        break;
                    }
                   
                }
                foreach (Hex neighbor in hex.Neighbors())
                {
                    if (neighbor.Terrain.SoilType == SoilType.Clay)
                    {
                        hexScores[hex] += 10;
                        break;
                    }
                }
            }

            selected = hexScores.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;

            return selected;
        }
    }
}