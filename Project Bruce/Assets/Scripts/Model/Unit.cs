using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace Bruce
{
    public class Unit : IPathUnit
    {
        public Unit(Country country, Hex hex)
        {
            CurrentHex = hex;
            CallToUnitController();
        }

        public Country country;

        Hex currentHex;
        Action<Unit> OnMoved;
        int moveTicks;

        public Queue<Hex> HexPath;
        public Hex CurrentHex { get { return currentHex; } set { OnMoved?.Invoke(this); currentHex = value; } }

        public void Tick()
        {
            ConsiderMove();
        }

        public void FindPath(Hex destination)
        {
            var path = Path.FindPath(this, CurrentHex, destination, Hex.CostEstimate);

            HexPath = new Queue<Hex>(path);
        }

        void ConsiderMove()
        {
            Debug.Log("ConsiderMove");
            if(HexPath == null || HexPath.Count == 0)
            {
                Debug.Log("No Next Hex");
                return;
            }
            else
            {
                if(moveTicks-- > 0)
                {
                    Debug.Log("moveTicks > 0");
                    return;
                }
                else
                {
                    CurrentHex = HexPath.Dequeue();
                    moveTicks = (int)CurrentHex.CostToEnter;

                    Debug.Log("Move");
                }
            }
        }
        void CallToUnitController()
        {
            UnitController.Instance.OnUnitCreated(this);
        }

        public float CostToEnterTile(IPathTile sourceTile, IPathTile destinationTile)
        {
            return 1f;
        }        
        public void RegisterOnMoved(Action<Unit> callback)
        {
            OnMoved += callback;
        }
    }
}