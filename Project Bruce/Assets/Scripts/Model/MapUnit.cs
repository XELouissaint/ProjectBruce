using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace Bruce
{
    public class MapUnit : IPathUnit
    {
        public MapUnit(Hex hex)
        {
            CurrentHex = hex;
            CallToUnitController();
            moveSpeed = 1;
            moveTicks = (int)(CurrentHex.CostToEnter / moveSpeed);
            
        }


        Hex currentHex;
        Action<MapUnit> OnMoved;
        int moveTicks;
        public float moveSpeed;

        public Queue<Hex> HexPath;
        public Hex CurrentHex { get { return currentHex; } set {  currentHex = value; } }

        public virtual void Tick()
        {
            ConsiderMove();
        }

        public virtual void FindPath(Hex destination)
        {
            var path = Path.FindPath(this, CurrentHex, destination, Hex.CostEstimate);

            HexPath = new Queue<Hex>(path);
        }

        public virtual void ConsiderMove()
        {
            Debug.Log("ConsiderMove");


            if (HexPath == null || HexPath.Count == 0)
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
                    moveTicks = (int)(CurrentHex.CostToEnter / moveSpeed);
                    OnMoved?.Invoke(this);
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
        public void RegisterOnMoved(Action<MapUnit> callback)
        {
            OnMoved += callback;
        }
    }

    public class PopUnit : MapUnit
    {
        public PopUnit(Country country, Hex hex) : base(hex)
        {
            this.Country = country;
        }

        public Country Country;
        public Pop Pop;

        public override void FindPath(Hex destination)
        {
            base.FindPath(destination);
        }
    }
}