using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bruce
{
    public class Settlement : IPopulationContainer
    {
        public Settlement(Country country, Hex hex)
        {
            this.country = country;
            this.hex = hex;
            hex.SetSettlement(this);
            Population = new Population(this, country.Population);
            Buildings = new List<Building>();
            Territory = new List<Hex>();
            Stockpile = new Stockpile();
            JobManager = new SettlementJobManager(this);

            Population.RegisterOnPopAdded(OnPopAdded);
            Population.RegisterOnPopRemoved(OnPopRemoved);

            Territory.Add(hex);

            if (country.Settlements.Count == 0)
            {
                AddBuilding(BuildingFactory.NewBaseCamp);
            }
        }
        public Hex hex;

        public int Housing;
        public Country country;
        public Population Population { get; set; }
        public List<Building> Buildings;
        public List<Hex> Territory;
        public Stockpile Stockpile;
        public SettlementJobManager JobManager;

        public void AddBuilding(Building building)
        {
            foreach(Job job in building.JobsProvided.Keys)
            {
                JobManager.OnJobAdded(job);
                JobManager.SetJobListLength(job,building.JobsProvided[job]);
            }
        }

        public void BeginConstructBuilding(Building building)
        {
            World.Instance.BuildingManager.UnderConstructionBuildings.Add(new BuildingPrototype(building,hex));
        }


        public void AddTerritory(Hex hex)
        {
            Territory.Add(hex);
            hex.BuildingManager.OnBuildingConstructed = AddBuilding;
            hex.Owner = this.country;
        }

        public void RemovePop(Pop pop)
        {

            Population.RemovePop(pop);
            JobManager.RemovePop(pop);
        }

        public void RefreshPopulation()
        {
            Population.RefreshPopulation();
        }

        public void Tick()
        {
            JobManager.ExecuteJobs();

        }

        public void OnPopAdded(Pop pop)
        {
            RefreshPopulation();
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

                break;
            }

        }

        void OnPopRemoved(Pop pop)
        {
            RefreshPopulation();
        }
    }
    
}