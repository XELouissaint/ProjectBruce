using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bruce
{
    public class PopGoalSystem 
    {
        public PopGoalSystem(Pop pop)
        {
            this.Pop = pop;
        }

        public Pop Pop;
        public PersonalGoal CurrentPersonalGoal;

        public void Tick()
        {
            if(CurrentPersonalGoal == null)
            {
                CurrentPersonalGoal = PersonalGoalFactory.GetRandomPersonalGoal();
                CurrentPersonalGoal.Target = Pop.Relations.OpinionDict.Keys.ToList()[World.RNG.Next(Pop.Relations.OpinionDict.Keys.ToList().Count)];
                Debug.Log(CurrentPersonalGoal);

            }
            if (CurrentPersonalGoal.CompleteRequirement.Invoke(Pop, CurrentPersonalGoal.Target))
            {
                CurrentPersonalGoal.OnComplete?.Invoke();
                CurrentPersonalGoal = null;
            }
        }
    }

    public class PersonalGoal
    {
        public string InteractionDescription(Pop pop, Interaction interaction)
        {
            if(pop == null || interaction == null)
            {
                return "NO VALID INTERACTION";

            }
            return string.Format(interaction.descriptionBase, pop.name, Target.name);

        }
        public Pop Target;

        public Interaction PreviousInteraction;

        public List<Interaction> InteractionOptions = new List<Interaction>();

        public Func<Pop,Pop, bool> CompleteRequirement;

        public Action OnComplete;
    }

    public static class PersonalGoalFactory
    {
        static PersonalGoal befriend;
        static PersonalGoal defeat;
        static PersonalGoal humiliate;
        static PersonalGoal shun;
        static PersonalGoal fornicate;

        public static PersonalGoal Befriend
        {
            get
            {
                if (befriend == null)
                {
                    befriend = new PersonalGoal();
                    befriend.CompleteRequirement = BefriendCompleteRequirement;
                    befriend.OnComplete = DebugOnComplete;
                    befriend.InteractionOptions.Add(InteractionFactory.PleasantTalk);

                }
                return befriend;
            }
        }

        public static PersonalGoal Defeat
        {
            get
            {
                if (defeat == null)
                {
                    defeat = new PersonalGoal();
                    defeat.CompleteRequirement = DefeatCompleteRequirement;
                    defeat.OnComplete = DebugOnComplete;
                    defeat.InteractionOptions.Add(InteractionFactory.Argument);
                }
                return defeat;
            }
        }




        public static PersonalGoal GetRandomPersonalGoal()
        {
            int rand = World.RNG.Next(0, 2);

            switch (rand)
            {
                case 0: return Befriend;
                case 1: return Defeat;
                default:
                    return Befriend;
            }
        }

        static bool BefriendCompleteRequirement(Pop interactor, Pop interactee)
        {
            if (interactor.Relations.OpinionDict.ContainsKey(interactee))
            {
                if (interactor.Relations.OpinionDict[interactee] > 75)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        static void DebugOnComplete()
        {
            Debug.Log("OnComplete");
        }

        static bool DefeatCompleteRequirement(Pop interactor, Pop interactee)
        {
            if (interactor.Relations.OpinionDict.ContainsKey(interactee))
            {
                if(interactor.Relations.OpinionDict[interactee] <= 25)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}