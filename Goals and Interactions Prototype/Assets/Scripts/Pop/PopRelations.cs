using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bruce {
    public class PopRelations
    {
        public PopRelations(Pop pop)
        {
            Pop = pop;
            OpinionDict = new Dictionary<Pop, double>();

            ModifyOpinion(pop, 0);
        }

        public Pop Pop;
        public Dictionary<Pop, double> OpinionDict;


        public void ModifyOpinion(Pop pop, double value)
        {
            if (OpinionDict.ContainsKey(pop) == false)
            {
                OpinionDict[pop] = 0;
            }
            OpinionDict[pop] += value;
        }

        public double AverageOpinionOfThis()
        {
            double total = 0;

            double avg = 0;
            foreach(Pop other in OpinionDict.Keys)
            {
                total += other.Relations.OpinionDict[Pop];
            }

            avg = total / OpinionDict.Keys.Count;

            return avg;
        }

    }
}