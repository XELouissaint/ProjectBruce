using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace Bruce
{
    public class Hex : IPathTile
    {
        public Hex(int gridX, int gridZ)
        {
            this.gridX = gridX;
            this.gridZ = gridZ;
            neighbors = new Hex[6];
            CostToEnter = 1f;
            Terrain = new HexTerrain(this);
            Ecosystem = new HexEcosystem(this);
            Economy = new HexEconomy(this);
        }
        public Vector3 Position;
        public int gridX;
        public int gridZ;
        Hex[] neighbors;
        public Country Owner;
        public Settlement Settlement;
        public float soilValue;
        public HexTerrain Terrain;
        public HexEcosystem Ecosystem;
        public HexEconomy Economy;

        public List<Unit> Units;

        public void SetSettlement(Settlement settlement)
        {
            this.Settlement = settlement;
            this.Owner = settlement.country;
        }

        public float CostToEnter;
        public void SetNeighbor(HexDirection dir, Hex plot)
        {
            neighbors[dir.Index] = plot;
            plot.neighbors[dir.Opposite.Index] = this;
        }
        public Hex GetNeighbor(HexDirection dir)
        {
            if (neighbors[dir.Index] == null)
            {
                return null;
            }
            return neighbors[dir.Index];
        }

        public Hex[] Neighbors()
        {
            List<Hex> actualNeighbors = new List<Hex>();

            for (int i = 0; i < neighbors.Length; i++)
            {
                Hex neighbor = neighbors[i];
                if (neighbor != null)
                {
                    actualNeighbors.Add(neighbor);
                }
            }
            return actualNeighbors.ToArray();
        }

        

        public HexDirection GetDirectionOfHex(Hex hex)
        {
            int index = Array.IndexOf(neighbors, hex);

            return HexDirection.GetDirectionFromIndex(index);
        }


        public IPathTile[] PathTileNeighbors()
        {
            return Neighbors();
        }

        public float AggregateCostSoFar(float currentCost, IPathTile source, IPathUnit unit)
        {
            return 1f;
        }

        public static float CostEstimate(IPathTile a, IPathTile b)
        {
            Hex newHex = (Hex)b;
            Hex oldHex = (Hex)a;
            float costEstimate = Map.HexDistance(newHex, oldHex);
            return costEstimate;
        }

    }
}