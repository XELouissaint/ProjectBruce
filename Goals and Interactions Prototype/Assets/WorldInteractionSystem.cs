using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bruce {
    public class WorldInteractionSystem
    {
        public WorldInteractionSystem(World world)
        {
            InteractScores = new List<InteractScore>();
            this.world = world;
            RefreshScoresList();
        }

        public List<InteractScore> InteractScores = new List<InteractScore>();
        public World world;

        public void RefreshScoresList()
        {
            InteractScores = new List<InteractScore>();
            foreach (Pop pop in world.Population)
            {
                InteractScores.Add(new InteractScore(pop));
            }
        }

        public void Tick()
        {

            RefreshScoresList();

            int i = 0;
            Debug.Log(InteractScores.Count);
            foreach (var score in InteractScores.OrderBy(s => s.Score))
            {

                if(i > world.maxInteractions)
                {
                    continue;
                }
                Interaction selected = null;
                PersonalGoal goal = score.pop.GoalSystem.CurrentPersonalGoal;

                if (goal != null)
                {
                    int rand = World.RNG.Next(0, goal.InteractionOptions.Count);
                    selected = goal.InteractionOptions[rand];
                }
                
                if (selected != null)
                {
                    selected.Execute(score.pop, goal.Target);
                    goal.PreviousInteraction = selected;
                    i++;
                }

                score.scoreOffset += 10;

            }
        }
    }

    public class InteractScore 
    {
        public InteractScore(Pop pop)
        {
            this.pop = pop;
        }
        public Pop pop;
        public double scoreOffset;
        public double Score
        {
            get
            {
                return pop.Relations.AverageOpinionOfThis() + scoreOffset;
            }
        }
    }
    public class Interaction
    {
        public Interaction(string desc, Action<Pop,Pop> onExecute)
        {
            descriptionBase = desc;
            OnExecute = onExecute;
        }

        public string descriptionBase;
        public Action<Pop, Pop> OnExecute;

        public void Execute(Pop interactor, Pop interactee)
        {
            OnExecute?.Invoke(interactor, interactee);
        }
    }
    public static class InteractionFactory
    {
        static Interaction pleasantTalk;
        static Interaction argument;
        //static Interaction fight;
        //static Interaction fornication;

        public static Interaction PleasantTalk 
        {
            get
            {
                if(pleasantTalk == null)
                {
                    string desc = "{0} and {1} had a great talk.";

                    pleasantTalk = new Interaction(desc, PleasantTalkOnExecute);
                }
                return pleasantTalk;
            }
        }

        public static Interaction Argument
        {
            get
            {
                if(argument == null)
                {
                    string desc = "{0} and {1} got into an argument";

                    argument = new Interaction(desc, ArgumentOnExecute);
                }
                return argument;
            }
        }

        public static void PleasantTalkOnExecute(Pop interactor, Pop interactee)
        {
            interactor.Relations.ModifyOpinion(interactee, 10);
            interactee.Relations.ModifyOpinion(interactor, 10);
        }

        public static void ArgumentOnExecute(Pop interactor, Pop interactee)
        {
            interactor.Relations.ModifyOpinion(interactee, -10);
            interactee.Relations.ModifyOpinion(interactor, -10);
        }
    }
}