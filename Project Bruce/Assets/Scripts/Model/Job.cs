using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bruce
{
    public interface IProcessable
    {
    }
    public class Job
    {

        public Job(string name, Action<Pop, Settlement> onExecute)
        {
            this.name = name;
            OnExecute += onExecute;
        }

        public string name;
        public Action<Pop, Settlement> OnExecute;

        public virtual void Execute(Pop pop, Settlement settlement)
        {
            OnExecute(pop,settlement);
        }

    }

    public class JobFactory
    {
        static JobFactory()
        {
        }
        
        public static Job GetWood
        {
            get 
            {
                return new Job("Get Wood",GetWoodOnExecute);
            }
        }
        public static Job GetFood
        {
            get
            {
                return new Job("Get Food",GetFoodOnExecute);
            }
        }

        public static void GetWoodOnExecute(Pop pop, Settlement settlement)
        {
            settlement.Stockpile.AddResource(GameIndex.Wood, 1);
        }
        public static void GetFoodOnExecute(Pop pop, Settlement settlement)
        {
            settlement.Stockpile.AddResource(GameIndex.Apple, 1);
        }
    }
}