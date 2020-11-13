using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bruce
{
    public class JobFactory
    {
        static JobFactory()
        {
        }

        static Job getWood;
        static Job chopTrees;
        static Job getFruit;
        static Job getStone;
        static Job mineStone;
        static Job getClay;
        static Job mineClay;
        static Job getWater;
        static Job GetWood
        {
            get
            {
                if (getWood == null)
                {
                    getWood = new Job("Get Wood", GetWoodOnExecute);

                    getWood.LifeStages.Add(PopLifeStage.Child);
                    getWood.LifeStages.Add(PopLifeStage.Teen);
                    getWood.LifeStages.Add(PopLifeStage.YoungAdult);
                    getWood.LifeStages.Add(PopLifeStage.Adult);
                }

                return getWood;
            }
        }
        static Job ChopTrees
        {
            get
            {
                if (chopTrees == null)
                {
                    chopTrees = new Job("Chop Trees", ChopTreesOnExecute);

                    chopTrees.LifeStages.Add(PopLifeStage.Teen);
                    chopTrees.LifeStages.Add(PopLifeStage.YoungAdult);
                    chopTrees.LifeStages.Add(PopLifeStage.Adult);
                }

                return chopTrees;
            }
        }
        static Job GetFruit
        {
            get
            {
                if (getFruit == null)
                {
                    getFruit = new Job("Get Food", GetFoodOnExecute);

                    getFruit.LifeStages.Add(PopLifeStage.Child);
                    getFruit.LifeStages.Add(PopLifeStage.Teen);
                    getFruit.LifeStages.Add(PopLifeStage.YoungAdult);
                    getFruit.LifeStages.Add(PopLifeStage.Adult);
                }

                return getFruit;
            }
        }
        static Job GetStone
        {
            get
            {
                if (getStone == null)
                {
                    getStone = new Job("Get Stone", GetStoneOnExecute);
                }

                getStone.LifeStages.Add(PopLifeStage.Teen);
                getStone.LifeStages.Add(PopLifeStage.YoungAdult);
                getStone.LifeStages.Add(PopLifeStage.Adult);

                return getStone;
            }
        }
        static Job MineStone
        {
            get
            {
                if (mineStone == null)
                {
                    mineStone = new Job("Mine Stone", MineStoneOnExecute);
                }

                mineStone.LifeStages.Add(PopLifeStage.YoungAdult);
                mineStone.LifeStages.Add(PopLifeStage.Adult);

                return mineStone;
            }
        }
        static Job GetClay
        {
            get
            {
                if (getClay == null)
                {
                    getClay = new Job("Get Clay", GetClayOnExecute);
                }

                getClay.LifeStages.Add(PopLifeStage.Child);
                getClay.LifeStages.Add(PopLifeStage.Teen);
                getClay.LifeStages.Add(PopLifeStage.YoungAdult);
                getClay.LifeStages.Add(PopLifeStage.Adult);

                return getClay;
            }
        }
        static Job MineClay
        {
            get
            {
                if (mineClay == null)
                {
                    mineClay = new Job("Mine Clay", MineClayOnExecute);
                }

                mineClay.LifeStages.Add(PopLifeStage.Teen);
                mineClay.LifeStages.Add(PopLifeStage.YoungAdult);
                mineClay.LifeStages.Add(PopLifeStage.Adult);

                return mineClay;
            }
        }
        static Job GetWater
        {
            get
            {
                if (getWater == null)
                {
                    getWater = new Job("Get Water", GetWaterOnExecute);
                }

                getWater.LifeStages.Add(PopLifeStage.Child);
                getWater.LifeStages.Add(PopLifeStage.Teen);
                getWater.LifeStages.Add(PopLifeStage.YoungAdult);
                getWater.LifeStages.Add(PopLifeStage.Adult);

                return getWater;
            }
        }

        public static Job GetResource(Resource resource)
        {
            if (resource == GameIndex.Wood)
            {
                return GetWood;
            }
            else if (resource == GameIndex.Fruit)
            {
                return GetFruit;
            }
            else if (resource == GameIndex.Stone)
            {
                return GetStone;
            }
            else if (resource == GameIndex.Clay)
            {
                return GetClay;
            }
            else if (resource == GameIndex.Water)
            {
                return GetWater;
            }
            else
            {
                return null;
            }
        }

        public static Job MineResource(Resource resource)
        {
            if (resource == GameIndex.Wood)
            {
                return ChopTrees;
            }
            else if (resource == GameIndex.Stone)
            {
                return MineStone;
            }
            else if (resource == GameIndex.Clay)
            {
                return MineClay;
            }
            else
            {
                return null;
            }
        }

        public static void GetWoodOnExecute(Pop pop, Settlement settlement)
        {
            settlement.Stockpile.AddResource(GameIndex.Wood, 1);
        }
        public static void ChopTreesOnExecute(Pop pop, Settlement settlement)
        {
            settlement.Stockpile.AddResource(GameIndex.Wood, 10);
        }

        public static void GetFoodOnExecute(Pop pop, Settlement settlement)
        {
            settlement.Stockpile.AddResource(GameIndex.Fruit, 1);
        }

        public static void GetStoneOnExecute(Pop pop, Settlement settlement)
        {
            settlement.Stockpile.AddResource(GameIndex.Stone, 1);
        }
        public static void MineStoneOnExecute(Pop pop, Settlement settlement)
        {
            settlement.Stockpile.AddResource(GameIndex.Stone, 3);
        }

        public static void GetClayOnExecute(Pop pop, Settlement settlement)
        {
            settlement.Stockpile.AddResource(GameIndex.Clay, 1);
        }
        public static void MineClayOnExecute(Pop pop, Settlement settlement)
        {
            settlement.Stockpile.AddResource(GameIndex.Clay, 3);
        }

        public static void GetWaterOnExecute(Pop pop, Settlement settlement)
        {
            settlement.Stockpile.AddResource(GameIndex.Water, 1);
        }
    }
}
