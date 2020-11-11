using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    public interface IPathTile
    {
        IPathTile[] PathTileNeighbors();

        float AggregateCostSoFar(float currentCost, IPathTile source, IPathUnit unit);
    }
}