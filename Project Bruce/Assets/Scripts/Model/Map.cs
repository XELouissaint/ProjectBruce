using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bruce
{
    public class Map
    {
        public Map(int width = 10, int height = 10)
        {
            Width = width;
            Height = height;

            HexGrid = new Hex[width, height];
        }

        public Hex[,] HexGrid;

        public int Width { get; protected set; }
        public int Height { get; protected set; }
        public void GenerateHexGrid()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int z = 0; z < Height; z++)
                {
                    Hex hex = HexGrid[x, z] = new Hex(x,z);
                    Vector3 position = new Vector3(HexHorizontalSpacing() * (x + z / 2f), 0, HexVerticalSpacing() * z);
                    hex.Position = position;
                    float noiseValue = Noise.GenerateNoiseMap(10, 10, World.RNG.Next(), .4f, 8, 12, 12, Vector3.zero)[x, z];
                    hex.soilValue = noiseValue;
                    AssignHexSoilTypeBasedOnNoise(hex, noiseValue);

                    hex.Ecosystem.GenerateRandomEcosystem();

                    if (x > 0)
                    {
                        hex.SetNeighbor(HexDirection.W, HexGrid[x - 1, z]);

                        if (z < Height - 1)
                        {
                            hex.SetNeighbor(HexDirection.NW, HexGrid[x - 1, z + 1]);

                        }
                    }
                    if (z > 0)
                    {
                        hex.SetNeighbor(HexDirection.SW, HexGrid[x, z - 1]);
                    }
                }
            }

            GenerateGrass();
        }

        #region HexMetrics
        static readonly float WIDTH_MULTIPLIER = Mathf.Sqrt(3) / 2;
        float radius = 1f;
        //float innerRadius = .66f;

        public float HexHeight()
        {
            return radius * 2;
        }

        public float HexWidth()
        {
            return WIDTH_MULTIPLIER * HexHeight();
        }

        public float HexVerticalSpacing()
        {
            return HexHeight() * 0.75f;
        }

        public float HexHorizontalSpacing()
        {
            return HexWidth();
        }

        public static float HexDistance(Hex a, Hex b)
        {
            return Mathf.Max(Mathf.Abs(a.Position.x - b.Position.x), Mathf.Abs(a.Position.z - b.Position.z));
        }
        #endregion

        public void AssignHexSoilTypeBasedOnNoise(Hex hex, float noiseValue)
        {

            if(noiseValue >= .66f)
            {
                hex.Terrain.SoilType = SoilType.Clay;
            } 
            else if(noiseValue >= .33f)
            {
                hex.Terrain.SoilType = SoilType.Loam;
            }
            else if(noiseValue >= 0)
            {
                hex.Terrain.SoilType = SoilType.Sand;
            }

        }

        public void GenerateGrass()
        {
            int randX = World.RNG.Next(0, Width);
            int randZ = World.RNG.Next(0, Height);
            Hex randomHex = HexGrid[randX, randZ];

            RecursivelySpreadGrass(randomHex);
        }

        public void RecursivelySpreadGrass(Hex randomHex)
        {
            double randGrowth = World.RNG.NextDouble();

            randomHex.Ecosystem.Grass = new Grass("grass", Mathf.Max(.1f, (float)randGrowth));

            foreach (Hex neighbor in randomHex.Neighbors())
            {
                float chanceToSpread = .25f;

                switch (neighbor.Terrain.SoilType)
                {
                    case SoilType.Sand:
                        chanceToSpread += 0f;
                        break;

                    case SoilType.Clay:
                        chanceToSpread += .25f;
                        break;

                    case SoilType.Loam:
                        chanceToSpread += .50f;
                        break;
                }

                double randSpread = World.RNG.NextDouble();
                
                if (randSpread < chanceToSpread)
                {
                }
            }
        }
    }
}