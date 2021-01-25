using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bruce
{
    public class HexEconomy
    {
        public HexEconomy(Hex hex)
        {
            this.hex = hex;
            ResourcesAvailable = new Dictionary<Resource, float>();

        }
        public Dictionary<Resource, float> ResourcesAvailable;

        Hex hex;

        public void EvaluateResources()
        {
            EvaluateResourcesFromTerrain();
            EvaluateResourcesFromEcosystem();
        }
        void EvaluateResourcesFromTerrain()
        {
            switch (hex.Terrain.SoilType)
            {
                case SoilType.Clay:
                    ResourcesAvailable[GameIndex.Clay] = 10000;
                    break;
            }

            if(hex.Terrain.WaterSource != WaterSource.Dry)
            {
                ResourcesAvailable[GameIndex.Water] = 10000;
            }

            float randStoneValue = Random.Range(0, 50);

            ResourcesAvailable[GameIndex.Stone] = randStoneValue;
        }
        void EvaluateResourcesFromEcosystem()
        {
            foreach (TreeBreed breed in hex.Ecosystem.Trees.Keys)
            {
                if (breed.cultivar is Cultivar cultivar)
                {

                    ResourcesAvailable[cultivar] = hex.Ecosystem.Trees[breed] * Random.Range(0f, breed.maxResources);
                }

                float woodValue = breed.maxSize * .5f * hex.Ecosystem.Trees[breed];

                ResourcesAvailable[GameIndex.Wood] = woodValue;
            }
        }

        

        float RemoveResource(Resource resource, float quantity)
        {
            float remainder = 0f;
            if (ResourcesAvailable.ContainsKey(resource) == false)
            {
                return quantity;
            }

            float newValue = ResourcesAvailable[resource] - quantity;

            if (newValue <= 0)
            {
                remainder = -newValue;
                ResourcesAvailable[resource] = 0;
            }

            

            return remainder;
        }
    }
}