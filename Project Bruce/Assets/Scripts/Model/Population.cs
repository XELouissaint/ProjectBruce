using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bruce
{
    public class Population
    {
        public Population(Population superPop = null)
        {
            Pops = new HashSet<Pop>();
            this.superPop = superPop;
            subPops = new HashSet<Population>();
        }

        public HashSet<Pop> Pops;

        public Population superPop;
        public HashSet<Population> subPops;

        public void GenerateRandomPopulation(int size, int minAge = 6, int maxAge = 35)
        {
            for (int i = 0; i < size; i++)
            {
                int age = World.RNG.Next(minAge, maxAge);
                PopGender gender = (PopGender)World.RNG.Next(2);
                Pop pop = new Pop(gender, age);
                Pops.Add(pop);
            }
        }

        public void RefreshPopulation()
        {
            if (superPop != null)
            {
                return;
            }
            List<Pop> newPopulation = new List<Pop>();
            foreach(Population population in subPops)
            {
                newPopulation.AddRange(population.Pops);
            }
            Pops = new HashSet<Pop>(newPopulation);
        }
    }

    public enum PopGender { Male, Female }
    public class Pop
    {
        public Pop()
        {
            ID = GameIndex.PopIDCounter++;
            name = ID.ToString();
        }

        public Pop(PopGender gender, int age) : this()
        {
            this.gender = gender;
            this.age = age;
        }

        int ID;
        public PopGender gender;
        public string name;
        public double age;


    }
}