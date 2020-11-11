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
            for (int i = 0; i < 1; i++)
            {
                Country country = new Country();
                country.Name = "Country " + 0;

                Hex hex = RecursivelyGetFreeHex();

                Settlement settlement = new Settlement(country, hex);
                settlement.Population.GenerateRandomPopulation(10);

                country.AddSettlement(settlement);
                settlement.RefreshPopulation();


                Countries.Add(country);
                ActiveCountries.Add(country);
                
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

            return hex;
        }
    }
}