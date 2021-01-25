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
        public Unit(Hex hex)
        {
            CurrentHex = hex;
            CallToUnitController();
            moveSpeed = 1;
            moveTicks = (int)(CurrentHex.CostToEnter / moveSpeed);
            Orders = new Queue<UnitOrder>();
        }


        Hex currentHex;
        Action<Unit> OnMoved;
        int moveTicks;
        public float moveSpeed;

        public Queue<Hex> HexPath;
        public Queue<UnitOrder> Orders;
        public Hex CurrentHex { get { return currentHex; } set {  currentHex = value; } }

        public virtual void Tick()
        {
            ExecuteNextOrder();


            ConsiderMove();
        }

        public virtual void FindPath(Hex destination)
        {
            var path = Path.FindPath(this, CurrentHex, destination, Hex.CostEstimate);
            Debug.Log("Find Path");
            HexPath = new Queue<Hex>(path);
        }

        public virtual void ExecuteNextOrder()
        {
            if(Orders.Count > 0)
            {
                Debug.Log("Orders");
                UnitOrder order = Orders.First();

                if(order.Destination == CurrentHex)
                {
                    Debug.Log("Execute");
                    Orders.Dequeue().OnExecute?.Invoke();
                    return;
                }

                FindPath(order.Destination);
            }
        }

        public virtual void ConsiderMove()
        {
            if (HexPath == null || HexPath.Count == 0)
            {

                Debug.Log("No Next Hex");
                return;
            }
            else
            {
                if(--moveTicks > 0)
                {
                    Debug.Log("moveTicks > 0");
                    return;
                }
                else
                {
                    CurrentHex = HexPath.Dequeue();
                    moveTicks = (int)(CurrentHex.CostToEnter / moveSpeed);
                    OnMoved?.Invoke(this);
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

    public class PopUnit : Unit
    {
        public PopUnit(Country country, Hex hex) : base(hex)
        {
            this.Country = country;
        }

        public Country Country;
        public Pop Pop;
        public UnitInventory Inventory;

        public override void FindPath(Hex destination)
        {
            base.FindPath(destination);
        }
    }

    public class AnimalUnit : Unit
    {
        public AnimalUnit(Country country, Hex hex) : base(hex)
        {
            this.Country = country;
        }

        public Country Country;
        public Animal Animal;
        public Action<AnimalUnit> OnEvadedHunt;
        public override void FindPath(Hex destination)
        {
            base.FindPath(destination);
        }

        public void EvadedHunt()
        {
            OnEvadedHunt?.Invoke(this);
        }

        public void RegisterOnEvadedHunt(Action<AnimalUnit> callback)
        {
            OnEvadedHunt += callback;
        }
    }

    public class UnitInventory
    {
        public UnitInventory(Unit unit)
        {
            Unit = unit;
            if (Unit is PopUnit popUnit)
            {
                Contents = new LimitList<Resource>(1);
            }
        }

        public Unit Unit;
        public LimitList<Resource> Contents;
        public Action<Resource> OnResourceAdded;
        
        public void AddContent(Resource resource)
        {
            if (Contents.Add(resource))
            {
                OnResourceAdded?.Invoke(resource);
            }
        }

        public void RegisterOnResourceAdded(Action<Resource> callback)
        {
            OnResourceAdded += callback;
        }
    }

}