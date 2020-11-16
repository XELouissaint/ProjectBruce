using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bruce
{
    public class Building
    {
        public Building(string name)
        {
            this.name = name;
            JobsProvided = new Dictionary<Job, int>();
            Housing = 0;
        }
        public string name;
        public int Housing;
        public Dictionary<Job, int> JobsProvided;
    }

    public static class BuildingFactory
    {
        public static Building NewBaseCamp
        {
            get
            {
                Building camp = new Building("Base Camp") { Housing = 6 };
                camp.JobsProvided.Add(JobFactory.GetResource(GameIndex.Fruit),2);
                camp.JobsProvided.Add(JobFactory.GetResource(GameIndex.Wood),2);
                camp.JobsProvided.Add(JobFactory.GetResource(GameIndex.Stone),2);
                camp.JobsProvided.Add(JobFactory.GetResource(GameIndex.Clay),2);
                camp.JobsProvided.Add(JobFactory.GetResource(GameIndex.Water),2);
                return camp;
            }
        }
    }
}
