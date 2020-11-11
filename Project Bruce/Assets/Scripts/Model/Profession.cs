using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bruce {
    public class Profession
    {
        public Profession(string name)
        {
            this.name = name;
        }

        public string name;
    }

    public static class ProfessionFactory
    {
        static ProfessionFactory()
        {
            Resident = new Profession("resident");
            Child = new Profession("child");
            Ward = new Profession("ward");
            Gatherer = new Profession("gatherer");
            Hunter = new Profession("hunter");
            Lumberor = new Profession("lumberor");
            Builder = new Profession("builder");

            AllProfessions = new List<Profession>();

            AllProfessions.Add(Resident);
            AllProfessions.Add(Child);
            AllProfessions.Add(Ward);
            AllProfessions.Add(Gatherer);
            AllProfessions.Add(Hunter);
            AllProfessions.Add(Lumberor);
            AllProfessions.Add(Builder);
        }
        public static List<Profession> AllProfessions;
        public static Profession Resident;
        public static Profession Child;
        public static Profession Ward;
        public static Profession Gatherer;
        public static Profession Hunter;
        public static Profession Lumberor;
        public static Profession Builder;
    }
}