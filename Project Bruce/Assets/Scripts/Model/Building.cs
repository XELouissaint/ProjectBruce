using System;
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

        public int timer;
    }

    public static class BuildingFactory
    {
        public static Building NewBaseCamp
        {
            get
            {
                Building building = new Building("Base Camp") { Housing = 6, timer = 7 };
                building.JobsProvided.Add(JobFactory.Gather,6);
                return building;
            }
        }

        public static Building NewFirePit
        {
            get
            {
                Building building = new Building("Fire Pit") { Housing = 0, timer = 3 };
                //building.JobsProvided.Add(JobFactory.TellStory, 1);
                // Roasting Jobs
                return building;
            }
        }

        public static Building NewMudPit 
        {
            get
            {
                Building building = new Building("Mud Pit") { Housing = 0, timer = 3 };
                //building.JobsProvided.Add(JobFactory.GetResource(GameIndex.Mud), 2);
                return building;
            }
        }

        public static Building NewMudHut
        {
            get
            {
                Building building = new Building("Mud Hut") { Housing = 2, timer = 10 };
                // Sewing Jobs
                // Art Jobs
                return building;
            }
        }
    }
}
