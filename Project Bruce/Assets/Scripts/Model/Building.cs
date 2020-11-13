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
            JobsProvided = new List<Job>();
            Housing = 0;
        }
        public string name;
        public int Housing;
        public List<Job> JobsProvided;
    }

    public static class BuildingFactory
    {
        public static Building NewBaseCamp
        {
            get
            {
                Building camp = new Building("Base Camp") { Housing = 6 };
                camp.JobsProvided.Add(JobFactory.GetResource(GameIndex.Fruit));
                camp.JobsProvided.Add(JobFactory.GetResource(GameIndex.Wood));
                camp.JobsProvided.Add(JobFactory.GetResource(GameIndex.Stone));
                camp.JobsProvided.Add(JobFactory.GetResource(GameIndex.Clay));
                camp.JobsProvided.Add(JobFactory.GetResource(GameIndex.Water));
                return camp;
            }
        }
    }
}
