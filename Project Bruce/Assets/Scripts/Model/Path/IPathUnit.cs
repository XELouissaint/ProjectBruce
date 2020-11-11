using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    public interface IPathUnit
    {
        float CostToEnterTile(IPathTile sourceTile, IPathTile destinationTile);
    }
}