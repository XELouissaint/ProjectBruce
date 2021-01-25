using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bruce
{
    public class Story
    {
        public IStorySubjectable hero;
        public IStorySubjectable villian;
        public string action;
        public int level;
        public int moral;

        public string description;
        public Action<Pop, int> OnExecute; 

        public void Execute(Pop teller)
        {
            OnExecute?.Invoke(teller, level);
        }
    }

    public static class StoryFactory 
    {

        public static string[] Actions = new string[]
        {
            "killed a {0}","seduced {0}","built {0}","discovered {0}"
        };

        public static Story MinorCourageousStory(Pop teller, Settlement settlement)
        {
            IStorySubjectable hero = teller.Nationality.Archive.RandomHero();
            IStorySubjectable villian = teller.Nationality.Archive.RandomVillian();
            Story story = new Story()
            {
                hero = hero,
                villian = villian,
                action = string.Format(Actions[0], villian.name),
                level = 1,
                moral = 1,
            };
            story.description = string.Format("{0} {1}", hero.name, story.action);
            DetermineStoryBonus(story, settlement.country);
            return story;
        }
        static void DetermineStoryBonus(Story story, Country country)
        {
            switch (story.moral)
            {
                case 0:
                    story.OnExecute += (hero, settlement) => DebugMoralOnExecute(hero);
                    break;
                case 1:
                    story.OnExecute += (hero, settlement) => CourageMoralOnExecute(hero, 1);
                    break;


            }

            if (country.Archive.ContainsMoral(story) == false)
            {
                World.GiveIdeaBoost(country, 1);
            }

        }


        static void CourageMoralOnExecute(Pop listener, int level)
        {
            ApplyCourageMoralModifier(listener, level);
        }
        static void DebugMoralOnExecute(Pop listener, int level = 0)
        {
            PopModifier newModifier = new PopModifier()
            {
                name = "Invalid Story",
                // timer = 1,
            };

            newModifier.Apply(listener.Modifiable);
        }

        static void ApplyCourageMoralModifier(Pop listener, int level = 1)
        {
            PopModifier newModifier = new PopModifier()
            {
                name = string.Format("Courageous Story{0}", new string('!', level)),
                FitnessEXPGain = .1f * level,
                ProwessEXPGain = .1f * level,
                // timer = 3 * level,
            };

            newModifier.Apply(listener.Modifiable);
        }

    }
}
