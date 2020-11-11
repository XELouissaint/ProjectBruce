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
        public void RefreshPopulation()
        {
            Debug.Log("Settlement Refresh");

            Population.RefreshPopulation();
        }

        public void Tick()
        {
            JobManager.ExecuteJobs();
        }



        public class SettlementJobManager
        {
            public SettlementJobManager(Settlement settlement)
            {
                Settlement = settlement;
                JobDictionary = new Dictionary<Job, List<Pop>>();
            }

            public Dictionary<Job, List<Pop>> JobDictionary;

            public Settlement Settlement;

            public void OnJobAdded(Job job)
            {
                if (JobDictionary.ContainsKey(job))
                {
                    return;
                }

                JobDictionary[job] = new List<Pop>();
            }

            public void AddPopToJob(Job job, Pop pop)
            {
                JobDictionary[job].Add(pop);
            }

            public void RemovePopToJob(Job job, Pop pop)
            {
                JobDictionary[job].Remove(pop);
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
        }
    }
    public class SettlementModifier : Modifier
    {
        public SettlementModifier(Settlement settlement)
        {
            this.settlement = settlement;
        }
        Settlement settlement;

        void MudHutOnTriggered()
        {
            settlement.Housing++;
        }

        void MudHutOnRemoved()
        {
            settlement.Housing--;
        }
    }
}