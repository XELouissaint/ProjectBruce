using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bruce
{
    public static class PopFactory
    {
        public static string[] namesMale = new string[]
        {
            "Jacob","Michael","Matthew","Jon","Christopher","Nicholas","Andrew","Joseph","Daniel","Tyler","William",
        };

        public static string[] namesFemale = new string[]
        {
            "Emily","Hannah","Matilda","Fatima","Sarah","Alexis","Samantha","Jessica","Eileen","Taylor",
        };

        public static Pop NewMale(int age)
        {
            int rand = World.RNG.Next(0, namesMale.Length);
            string name = namesMale[rand];
            Pop male = new Pop(PopGender.Male, name, age);
            return male;
        }
        public static Pop NewFemale(int age)
        {
            int rand = World.RNG.Next(0, namesFemale.Length);
            string name = namesFemale[rand];
            Pop female = new Pop(PopGender.Female, name, age);
            return female;
        }

        public static Pop RandomPop(int age)
        {
            PopGender gender = (PopGender)World.RNG.Next(2);

            switch (gender)
            {
                case PopGender.Male:
                    return NewMale(age);

                default:
                    return NewFemale(age);

            }
        }
    }
}
