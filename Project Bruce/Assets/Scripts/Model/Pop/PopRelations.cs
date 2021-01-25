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

            AddPop(pop, 0);
        }

        public Pop Pop;
        public Dictionary<Pop, double> OpinionDict;

        public void AddPop(Pop pop, double value)
        {
            OpinionDict.Add(pop, value);

        }

        public void ModifyOpinion(Pop pop, double value)
        {
            OpinionDict[pop] += value;
        }

    }
}