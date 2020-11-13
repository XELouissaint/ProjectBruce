using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bruce
{
    public class Settlement
    {
        public Settlement(Country country, Hex hex)
        {
            this.country = country;
            this.hex = hex;
            hex.SetSettlement(this);
            Population = new Population(country.Population);
            Buildings = new List<Building>();
            Territory = new List<Hex>();
            Stockpile = new Stockpile();
            JobManager = new SettlementJobManager(this);

            Population.RegisterOnPopAdded(OnPopAdded);


            Territory.Add(hex);

            if (country.Settlements.Count == 0)
            {
                AddBuilding(BuildingFactory.NewBaseCamp);
            }
        }
        public Hex hex;

        public int Housing;
        public Country country;
        public Population Population;
        public List<Building> Buildings;
        public List<Hex> Territory;
        public Stockpile Stockpile;
        public SettlementJobManager JobManager;

        public void AddBuilding(Building building)
        {
            Buildings.Add(building);

            foreach(Job job in building.JobsProvided)
            {
                JobManager.OnJobAdded(job);
            }
        }

        public void AddTerritory(Hex hex)
        {
            Territory.Add(hex);
            hex.Owner = this.country;
            Debug.Log("Set Territory");
        }
        public void RefreshPopulation()
        {
            Debug.Log("Settlement Refresh");

            Population.RefreshPopulation();
        }

        public void Tick()
        {
            JobManager.ExecuteJobs();
        }

        public void OnPopAdded(Pop pop)
        {
            for (int i = 0; i < 100; i++)
            {                
                if(JobManager.AssignPopToUnWorkedJob(pop))
                {
                    return;
                }
            }
            
            var jobDict = JobManager.JobDictionary;

            for (int i = 0; i < 100; i++)
            {
                int rand = World.RNG.Next(0, jobDict.Keys.Count);

                Job randJob = jobDict.ElementAt(rand).Key;


                if(JobManager.AddPopToJob(randJob, pop))
                {
                    return;
                }

            }

        }        
    }
    public class SettlementJobManager
    {
        public SettlementJobManager(Settlement settlement)
        {
            Settlement = settlement;
            JobDictionary = new Dictionary<Job, LimitList<Pop>>();
        }

        public Dictionary<Job, LimitList<Pop>> JobDictionary;
        public Dictionary<Job, int> MaxWorkersPerJob;
        public Settlement Settlement;

        public bool JobsUnWorked()
        {
            bool value = false;

            foreach (Job job in JobDictionary.Keys)
            {
                if(JobDictionary[job].Count == 0)
                {
                    return true;
                }
            }

            return value;
        }

        public void OnJobAdded(Job job)
        {
            if (JobDictionary.ContainsKey(job))
            {
                return;
            }

            JobDictionary[job] = new LimitList<Pop>(5);
        }

        public bool AddPopToJob(Job job, Pop pop)
        {
            bool result = JobDictionary[job].Add(pop);
            foreach (Job key in JobDictionary.Keys)
            {
                if (JobDictionary[key].Contains(pop) && result == false)
                {
                    RemovePopFromJob(key, pop);
                }
            }
            return result;
        }

        public bool RemovePopFromJob(Job job, Pop pop)
        {
            return JobDictionary[job].Remove(pop);
        }

        public void ExecuteJobs()
        {
            foreach (Job job in JobDictionary.Keys)
            {
                foreach (Pop pop in JobDictionary[job])
                {
                    job.Execute(pop, Settlement);
                }
            }
        }

        public bool AssignPopToUnWorkedJob(Pop pop)
        {
            foreach (Job job in JobDictionary.Keys)
            {
                Debug.Log("Count: " + JobDictionary[job].Count);
                if (JobDictionary[job].Count == 0)
                {
                   bool add = AddPopToJob(job, pop);

                    return add;
                }
            }

            return false;
        }
    }
}