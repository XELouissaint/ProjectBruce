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
            LifeStages = new List<PopLifeStage>();
            Requirements = (settlement) => NoRequirements;
        }

        public List<PopLifeStage> LifeStages;
        public string name;
        public Action<Pop, Settlement> OnExecute;
        public Func<Settlement, bool> Requirements;

        bool NoRequirements = true;

        public virtual void Execute(Pop pop, Settlement settlement)
        {
            OnExecute(pop,settlement);
        }

    }
}