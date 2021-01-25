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
            Instance = this;
            if (map == null)
            {
                Map = new Map();
            }
            else
            {
                Map = map;
            }
            BuildingManager = new WorldBuildingManager();
            InteractionSystem = new WorldInteractionSystem(this);
            Countries = new HashSet<Country>();
            ActiveCountries = new HashSet<Country>();
            CalendarSystem = CalendarSystem.Gregorian();
            Map.GenerateHexGrid();
            SpawnStartCountries();

        }

        public static System.Random RNG = new System.Random();

        public static World Instance;

        public Action<Pop> OnPopAdded;

        public Map Map;
        public CalendarSystem CalendarSystem;
        public WorldBuildingManager BuildingManager;
        public WorldInteractionSystem InteractionSystem;
        public Country Nature;
        public HashSet<Country> Countries;
        public HashSet<Country> ActiveCountries;

        public void Tick()
        {
            CalendarSystem.TickDay();
            foreach (Country country in ActiveCountries)
            {
                country.Tick();
            }

            Nature.Tick();

            BuildingManager.Tick();
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

                settlement.Population.RegisterOnPopAdded(InteractionSystem.OnPopAdded);
                settlement.Population.GenerateRandomPopulation(10);

                country.AddSettlement(settlement);
                country.RefreshPopulation();
                settlement.RefreshPopulation();

                Countries.Add(country);
                ActiveCountries.Add(country);

                Debug.Log("Keys: " + InteractionSystem.InteractScore.Keys.Count);
            }
        }

        public void SpawnStartAnimals()
        {
            Animal animal = new Animal(AnimalFactory.SheepBreed);

            AnimalUnit animalUnit = new AnimalUnit(Nature, RecursivelyGetFreeHex());
            animalUnit.Animal = animal;
            animalUnit.RegisterOnEvadedHunt(FleeToRandomHex);

            Nature.UnitManager.AddUnit(animalUnit);
        }

        void FleeToRandomHex(AnimalUnit animalUnit)
        {
            int xOffset = RNG.Next(-2,3);
            int zOffset = RNG.Next(-2,3);

            int randX = animalUnit.CurrentHex.gridX + xOffset;
            int randZ = animalUnit.CurrentHex.gridZ + zOffset;

            Hex newHex = Map.GetHexAt(randX, randZ);

            if (newHex != null)
            {
                animalUnit.FindPath(newHex);

                Debug.Log(randX +"," + randZ);
                Debug.Log(animalUnit.HexPath.Count);
            }
            else
            {
                FleeToRandomHex(animalUnit);
            }
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

        public static void GiveIdeaBoost(Country country, float multiplier)
        {
            country.SciencePool.PoolValue += 50 * (1 + multiplier);
        }
    }
}