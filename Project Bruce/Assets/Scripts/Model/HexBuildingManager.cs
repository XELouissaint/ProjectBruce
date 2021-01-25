using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bruce
{
    public class HexBuildingManager
    {
        public HexBuildingManager(Hex hex)
        {
            Hex = hex;
            ConstructedBuildings = new List<Building>();
        }

        public Hex Hex;
        public Action<Building> OnBuildingConstructed;

        public List<Building> ConstructedBuildings;
        
    }

    public class BuildingPrototype
    {
        public BuildingPrototype(Building building, Hex hex)
        {
            this.building = building;
            this.hex = hex;
            this.timer = building.timer;
        }
        public Building building;
        public Hex hex;
        public int timer;
    }
}