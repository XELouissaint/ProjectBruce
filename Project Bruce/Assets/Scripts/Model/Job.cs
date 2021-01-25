using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bruce
{
    public class Job
    {

        public Job(string name)
        {
            this.name = name;
            this.Modes = new List<JobMode>();
            this.Modes.Add(JobFactory.NoMode);
        }

        public string name;

        public List<JobMode> Modes;

    }

    public class JobMode 
    {
        public JobMode(string name, Action<Pop,Settlement> onExecute)
        {

            this.name = name;
            OnExecute += onExecute;
            LifeStages = new List<PopLifeStage>();
            Requirements = (settlement) => NoRequirements;
        }

        public Job Job;


        public List<PopLifeStage> LifeStages;
        public string name;
        public Action<Pop, Settlement> OnExecute;
        public Func<Settlement, bool> Requirements;

        bool NoRequirements = true;

        public virtual void Execute(Pop pop, Settlement settlement)
        {
            OnExecute(pop, settlement);
        }

    }
}