using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bruce
{

    public enum PopGender { Male, Female }
    public class Pop
    {
        public Pop(int age)
        {
            ID = GameIndex.PopIDCounter++;
            this.age = age;
            LifeCycle = new PopLifeCycle(this);
        }

        public Pop(PopGender gender, string name, int age) : this(age)
        {
            this.gender = gender;
            this.name = name;
        }

        int ID;
        public PopGender gender;
        public PopLifeCycle LifeCycle;
        public string name;
        public double age;

        public void OnDay()
        {
            LifeCycle.OnDay();
        }
    }
    public enum PopLifeStage { Baby, Child, Teen, YoungAdult, Adult, Zombie }

    public class PopLifeCycle
    {
        public PopLifeCycle(Pop pop)
        {
            Pop = pop;

            AssignLifeStage();
        }

        public PopLifeStage LifeStage;
        public Pop Pop;

        public void OnDay()
        {
            Pop.age += 1/360;
            AssignLifeStage();
        }

        void AssignLifeStage()
        {
            if (Pop.age > 25)
            {
                LifeStage = PopLifeStage.Adult;
            }
            else if (Pop.age > 16)
            {
                LifeStage = PopLifeStage.YoungAdult;
            }
            else if (Pop.age > 12)
            {
                LifeStage = PopLifeStage.Teen;
            }
            else if (Pop.age > 6)
            {
                LifeStage = PopLifeStage.Child;
            }
            else if (Pop.age > 0)
            {
                LifeStage = PopLifeStage.Baby;
            }
            else
            {
                LifeStage = PopLifeStage.Zombie;
            }
        }
    }

    public class PopStats
    {
        public PopStats(Pop pop)
        {
            Pop = pop;
        }
        public Pop Pop;

        public Stat Creativity;
        public Stat Wit;
        public Stat Logic;

        public Stat Fitness;
        public Stat Dexterity;
        public Stat Prowess;

        public Stat Charm;
        public Stat Looks;
        public Stat Persuasion;
    }
}
