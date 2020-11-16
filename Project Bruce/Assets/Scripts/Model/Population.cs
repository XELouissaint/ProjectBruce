using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bruce
{
    public interface IPopulationContainer
    {
        void RefreshPopulation();
    }

    public class Population
    {
        public Population(IPopulationContainer rep, Population superPop)
        {
            this.rep = rep;
            Pops = new HashSet<Pop>();
            this.superPop = superPop;
            subPops = new HashSet<Population>();
        }

        public  HashSet<Pop> Pops;
        public Action<Pop> OnPopAdded;
        public Action<Pop> OnPopRemoved;

        public IPopulationContainer rep;

        public Population superPop;
        public HashSet<Population> subPops;

        public void GenerateRandomPopulation(int size, int minAge = 6, int maxAge = 35)
        {
            for (int i = 0; i < size; i++)
            {
                int age = World.RNG.Next(minAge, maxAge);

                Pop pop = PopFactory.RandomPop(age);
                AddPop(pop);
            }
        }

        public void AddPop(Pop pop)
        {
            Pops.Add(pop);
            OnPopAdded?.Invoke(pop);
        }
        public void RemovePop(Pop pop)
        {
            Pops.Remove(pop);
            OnPopRemoved?.Invoke(pop);
        }

        public void RefreshPopulation()
        {
            if (superPop != null)
            {
                superPop.SuperPopRefreshPopulation();
            }
            else
            {
                SubPopRefreshPopulation();
            }
        }

        void SuperPopRefreshPopulation()
        {

            List<Pop> newPopulation = new List<Pop>();
            foreach (Population population in subPops)
            {
                population.SubPopRefreshPopulation();
                newPopulation.AddRange(population.Pops);
            }

            newPopulation.AddRange(Pops.Where(pop => newPopulation.Contains(pop) == true));

            Pops = new HashSet<Pop>(newPopulation);

            rep.RefreshPopulation();
        }
        void SubPopRefreshPopulation()
        {

        }

        public void RegisterOnPopAdded(Action<Pop> callback)
        {
            OnPopAdded += callback;
        }
        public void RegisterOnPopRemoved(Action<Pop> callback)
        {
            OnPopRemoved += callback;
        }
    }
}
