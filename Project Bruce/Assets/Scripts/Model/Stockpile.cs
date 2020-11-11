using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bruce
{
    public class Stockpile
    {
        public Stockpile()
        {
            Resources = new Dictionary<Resource, float>();
            ResourcesToExpiration = new Dictionary<Resource, Dictionary<int, float>>();
        }
        public Dictionary<Resource, float> Resources;
        public Dictionary<Resource, Dictionary<int, float>> ResourcesToExpiration;


        public void AddResource(Resource resource, float value)
        {
            if(Resources.ContainsKey(resource) == false)
            {
                Resources[resource] = value;
            }
            else
            {
                Resources[resource] += value;
            }

            if(Resources[resource] <= 0)
            {
                Resources.Remove(resource);
            }
        }
    }
}