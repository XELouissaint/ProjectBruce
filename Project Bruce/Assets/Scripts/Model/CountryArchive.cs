using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bruce
{
    public class CountryArchive
    {
        public CountryArchive(Country country)
        {
            Country = country;
            Stories = new List<Story>();
            Heroes = new List<IStorySubjectable>();
            Villians = new List<IStorySubjectable>();
            Heroes.AddRange(country.Population.Pops);
            Monster = new Animal(AnimalFactory.DebugBreed);
            Villians.Add(Monster);
        }
        public Country Country;

        public List<Story> Stories;
        public List<IStorySubjectable> Heroes;
        public List<IStorySubjectable> Villians;
        public Animal Monster;
        internal bool ContainsMoral(Story story)
        {
            return Stories.Any(s => s.moral == story.moral);
        }

        public IStorySubjectable RandomHero()
        {
            int rand = World.RNG.Next(Heroes.Count);
            return Heroes[rand];
        }
        public IStorySubjectable RandomVillian()
        {
            int rand = World.RNG.Next(Villians.Count);
            return Villians[rand];
        }
    }

}