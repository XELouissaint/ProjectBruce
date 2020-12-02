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
        public WaterSource WaterSource;
    }

    public class WaterSource
    {
        public string name;
        public bool running;
        public float volume;

        static WaterSource river;
        static WaterSource lake;
        static WaterSource aquifer;
        static WaterSource dry;
        public static WaterSource River
        {
            get
            {
                if(river == null)
                {
                    river = new WaterSource()
                    {
                        name = "river",
                        running = true,
                        volume = 2,
                    };
                }

                return river;

            }
        }
        public static WaterSource Lake
        {
            get 
            {
                if (lake == null)
                {
                    lake = new WaterSource()
                    {
                        name = "lake",
                        running = false,
                        volume = 10,
                    };
                }
                return lake;
            }
        }
        public static WaterSource Aquifer
        {
            get
            {
                if (aquifer == null)
                {
                    aquifer = new WaterSource()
                    {
                        name = "aquifer",
                        running = false,
                        volume = 10,
                    };
                }
                return aquifer;
            }
        }
        public static WaterSource Dry
        {
            get
            {
                if (dry == null)
                {
                    dry = new WaterSource()
                    {
                        name = "dry",
                        running = false,
                        volume = 0,
                    };

                }
                    return dry;
            }
        }
    }
}