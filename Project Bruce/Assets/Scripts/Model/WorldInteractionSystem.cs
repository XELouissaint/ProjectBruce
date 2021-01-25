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
            InteractScore = new Dictionary<Pop, int>();
        }

        public Dictionary<Pop, int> InteractScore;



        public void OnPopAdded(Pop pop)
        {
            Debug.Log("OnPopAdded");
            InteractScore[pop] = 0;
        }
    }
    public class Interaction
    {
        public Interaction(string desc, Action<Pop,Pop> onExecute)
        {
            descriptionBase = desc;
            OnExecute = onExecute;
        }

        string descriptionBase;

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