using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    public static class Path
    {
        public static T[] FindPath<T>(IPathUnit unit, T startTile, T endTile, CostEstimateDelegate costEstimate) where T : IPathTile
        {


            if(unit == null || startTile == null || endTile == null)
            {
                Debug.Log("Null values passed to FindPath");
                return null;
            }

            Path_AStar<T> resolver = new Path_AStar<T>(unit, startTile, endTile, costEstimate);

            resolver.DoWork();

            return resolver.GetArray();

        }
    }

    public delegate float CostEstimateDelegate(IPathTile a, IPathTile b);
}