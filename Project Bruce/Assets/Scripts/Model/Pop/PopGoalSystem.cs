using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bruce
{
    public class PopGoalSystem : MonoBehaviour
    {
        public PopGoalSystem(Pop pop)
        {
            this.Pop = pop;
        }

        public Pop Pop;
        public PersonalGoal CurrentPersonalGoal;
    }

    public class PersonalGoal
    {
        public string Description;

        public Func<Pop,Pop, bool> CompleteRequirement;

        public List<Interaction> InteractionOptions;
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
                if(befriend == null)
                {
                    befriend = new PersonalGoal();
                    befriend.CompleteRequirement = BefriendCompleteRequirement;

                    befriend.InteractionOptions.Add(InteractionFactory.PleasantTalk);
                }
                return befriend;
            }
        }

        static bool BefriendCompleteRequirement(Pop interactor, Pop interactee)
        {
            if (interactor.Relations.OpinionDict.ContainsKey(interactee))
            {
                if(interactor.Relations.OpinionDict[interactee] > 75)
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