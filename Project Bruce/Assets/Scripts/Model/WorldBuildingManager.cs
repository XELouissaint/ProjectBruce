using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bruce
{
    public class WorldBuildingManager
    {
        public List<BuildingPrototype> UnderConstructionBuildings = new List<BuildingPrototype>();

        public void Tick()
        {
            for (int i = UnderConstructionBuildings.Count - 1; i >= 0; i--)
            {
                BuildingPrototype prototype = UnderConstructionBuildings[i];
                prototype.timer--;
                if (prototype.timer <= 0)
                {
                    if(prototype.hex != null)
                    {
                        prototype.hex.BuildingManager.ConstructedBuildings.Add(prototype.building);
                    }

                    UnderConstructionBuildings.Remove(prototype);
                }
            }
        }
    }
}