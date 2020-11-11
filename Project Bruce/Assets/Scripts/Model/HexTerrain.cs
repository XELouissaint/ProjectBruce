using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bruce
{
    public enum SoilType { Clay, Sand, Loam }
    public class HexTerrain
    {
        public HexTerrain(Hex hex) 
        {
            Hex = hex;
        }

        public HexTerrain(Hex hex, SoilType soilType, float centerElevation) : this(hex)
        {
            SoilType = soilType;
            this.centerElevation = centerElevation;
        }

        public Hex Hex;

        public float centerElevation;

        public SoilType SoilType;
    }
}