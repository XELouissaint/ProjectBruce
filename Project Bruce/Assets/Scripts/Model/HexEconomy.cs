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

            EvaluateResourcesFromEcosystem();

        }
        public Dictionary<Resource, float> ResourcesAvailable;

        Hex hex;
        void EvaluateResourcesFromEcosystem()
        {
            foreach (TreeBreed breed in hex.Ecosystem.Trees.Keys)
            {
                if (breed.cultivar is Cultivar cultivar && cultivar != GameIndex.None)
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