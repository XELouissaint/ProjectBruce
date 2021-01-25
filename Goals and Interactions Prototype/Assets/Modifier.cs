using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bruce
{
    public struct PopModifier
    {
        public string name;

        public float FoodNeed;

        public float CreativityEXPGain;
        public float WitEXPGain;
        public float LogicEXPGain;
        public float FitnessEXPGain;
        public float DexterityEXPGain;
        public float ProwessEXPGain;
        public float CharmEXPGain;
        public float LooksEXPGain;
        public float PersuasionEXPGain;

        public void Apply(PopModifiable modifiable)
        {
            modifiable.Modifiers.Add(this);

            modifiable.FoodNeed += FoodNeed;

            modifiable.CreativityEXPGain += CreativityEXPGain;
            modifiable.WitEXPGain += WitEXPGain;
            modifiable.LogicEXPGain += LogicEXPGain;
            modifiable.FitnessEXPGain += FitnessEXPGain;
            modifiable.DexterityEXPGain += DexterityEXPGain;
            modifiable.ProwessEXPGain += ProwessEXPGain;
            modifiable.CharmEXPGain += CharmEXPGain;
            modifiable.LooksEXPGain += LooksEXPGain;
            modifiable.PersuasionEXPGain += PersuasionEXPGain;
        }
    }

    

   
}