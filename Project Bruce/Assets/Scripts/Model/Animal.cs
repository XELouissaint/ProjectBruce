using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bruce
{
    public enum AnimalDiet { Herbivore, Omnivore, Carnivore }
    public class Animal : IStorySubjectable
    {
        public Animal(AnimalBreed breed)
        {
            this.Breed = breed;
        }

        public AnimalBreed Breed;
        public string name { get; set; }
    }

    public class AnimalBreed
    {
        public AnimalBreed()
        {
        }
        public string name;
        public float huntEvasion;
        public float huntRetaliation;
        public AnimalDiet animalDiet;
    }

    public static class AnimalFactory
    {
        public static AnimalBreed SheepBreed;
        public static AnimalBreed DebugBreed;
        static AnimalFactory()
        {
            SheepBreed = new AnimalBreed()
            {
                name = "sheep",
                huntEvasion = .66f,
                huntRetaliation = 0f,
                animalDiet = AnimalDiet.Herbivore,
            };
            DebugBreed = new AnimalBreed()
            {
                name = "monster",
                animalDiet = AnimalDiet.Omnivore
            };
        }
    }
}