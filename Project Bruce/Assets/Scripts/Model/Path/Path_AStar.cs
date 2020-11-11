using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    public class Path_AStar<T> where T : IPathTile
    {
        public Path_AStar(IPathUnit unit, T startTile, T endTile, CostEstimateDelegate costEstimate)
        {
            this.unit = unit;
            this.startTile = startTile;
            this.endTile = endTile;
            this.costEstimate = costEstimate;
        }

        IPathUnit unit;
        T startTile;
        T endTile;
        CostEstimateDelegate costEstimate;

        Queue<T> path;

        public void DoWork()
        {

            path = new Queue<T>();

            HashSet<T> closedSet = new HashSet<T>();

            PathfindingPriorityQueue<T> openSet = new PathfindingPriorityQueue<T>();
            openSet.Enqueue(startTile,0);

            Dictionary<T, T> cameFrom = new Dictionary<T, T>();

            Dictionary<T, float> gScore = new Dictionary<T, float>();
            gScore[startTile] = 0;

            Dictionary<T, float> fScore = new Dictionary<T, float>();
            fScore[startTile] = costEstimate(startTile, endTile);

            while(openSet.Count > 0)
            {
                //Debug.Log("openSet > 0");
                T current = openSet.Dequeue();

                if(System.Object.ReferenceEquals(current, endTile))
                {
                    ReconstructPath(cameFrom, current);
                }

                closedSet.Add(current);

                foreach(T edgeNeighbor in current.PathTileNeighbors())
                {
                    //Debug.Log("Found Neighbor");

                    T neighbor = edgeNeighbor;

                    if (closedSet.Contains(neighbor))
                    {
                        continue;
                    }

                    float totalCost = neighbor.AggregateCostSoFar(gScore[current], current, unit);

                    if(totalCost < 0)
                    {
                        //Debug.Log("totalCost < 0");

                        continue;
                    }

                    float tentativeGScore = totalCost;

                    
                    if (openSet.Contains(neighbor) && tentativeGScore >= gScore[neighbor])
                    {
                        //Debug.Log("openSet Contains Neighbor");

                        continue;
                    }

                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeGScore;
                    fScore[neighbor] = gScore[neighbor] + costEstimate(neighbor, endTile);

                    openSet.EnqueueOrUpdate(neighbor,fScore[neighbor]);
                }
            }
        }

        void ReconstructPath(Dictionary<T,T> cameFrom, T current)
        {
            Queue<T> totalPath = new Queue<T>();
            totalPath.Enqueue(current);

            while (cameFrom.ContainsKey(current))
            {
                current = cameFrom[current];
                totalPath.Enqueue(current);
            }

            path = new Queue<T>(totalPath.Reverse());
            path.Dequeue();
        }

        public T[] GetArray()
        {
            return path.ToArray();
        }
    }
}