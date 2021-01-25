using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Bruce
{
    public class UnitOrder
    {
        public UnitOrder(Unit unit, Hex destination, Action onExecute)
        {
            this.Unit = unit;
            this.Destination = destination;
            this.OnExecute += onExecute;
        }

        public Unit Unit;

        public Action OnExecute;

        public Hex Destination;
    }

    public static class UnitOrderFactory
    {
        public static UnitOrder HuntAnimal(PopUnit popUnit, AnimalUnit animalUnit)
        {
            UnitOrder order = new UnitOrder(popUnit, animalUnit.CurrentHex, () => { HuntAnimalOnExecute(popUnit, animalUnit); });

            return order;
        }

        public static void HuntAnimalOnExecute(PopUnit unit, AnimalUnit animalUnit)
        {
            float chanceToCatch = animalUnit.Animal.Breed.huntEvasion;
            double rand = World.RNG.NextDouble();

            if(rand < chanceToCatch)
            {
                unit.Inventory.AddContent(new Corpse(animalUnit.Animal));
            }
            else
            {
                animalUnit.EvadedHunt();
                unit.Orders.Enqueue(HuntAnimal(unit, animalUnit));
            }
        }
    }
}