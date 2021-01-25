using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bruce
{
    public static class JobFactory
    {
        static JobFactory()
        {
        }

        static JobMode getWood;
        static JobMode chopTrees;
        static JobMode getFruit;
        static JobMode getStone;
        static JobMode mineStone;
        static JobMode getClay;
        static JobMode mineClay;
        static JobMode getWater;
        static JobMode getMud;
        static JobMode retellStory;
        static JobMode noMode;

        static Job gather;
        static Job mine;
        static Job storyTell;

        #region Properties
        static JobMode GatherWood
        {
            get
            {
                if (getWood == null)
                {
                    getWood = new JobMode("Wood", GetWoodOnExecute);

                    getWood.LifeStages.Add(PopLifeStage.Child);
                    getWood.LifeStages.Add(PopLifeStage.Teen);
                    getWood.LifeStages.Add(PopLifeStage.YoungAdult);
                    getWood.LifeStages.Add(PopLifeStage.Adult);
                }

                return getWood;
            }
        }
        static JobMode ChopTrees
        {
            get
            {
                if (chopTrees == null)
                {
                    chopTrees = new JobMode("Chop Trees", ChopTreesOnExecute);

                    chopTrees.LifeStages.Add(PopLifeStage.Teen);
                    chopTrees.LifeStages.Add(PopLifeStage.YoungAdult);
                    chopTrees.LifeStages.Add(PopLifeStage.Adult);
                }

                return chopTrees;
            }
        }
        static JobMode GatherFruit
        {
            get
            {
                if (getFruit == null)
                {
                    getFruit = new JobMode("Fruit", GetFoodOnExecute);

                    getFruit.LifeStages.Add(PopLifeStage.Child);
                    getFruit.LifeStages.Add(PopLifeStage.Teen);
                    getFruit.LifeStages.Add(PopLifeStage.YoungAdult);
                    getFruit.LifeStages.Add(PopLifeStage.Adult);
                }

                return getFruit;
            }
        }
        static JobMode GatherStone
        {
            get
            {
                if (getStone == null)
                {
                    getStone = new JobMode("Stone", GetStoneOnExecute);
                    getStone.LifeStages.Add(PopLifeStage.Teen);
                    getStone.LifeStages.Add(PopLifeStage.YoungAdult);
                    getStone.LifeStages.Add(PopLifeStage.Adult);
                }


                return getStone;
            }
        }
        static JobMode MineStone
        {
            get
            {
                if (mineStone == null)
                {
                    mineStone = new JobMode("Mine Stone", MineStoneOnExecute);
                    mineStone.LifeStages.Add(PopLifeStage.YoungAdult);
                    mineStone.LifeStages.Add(PopLifeStage.Adult);
                }


                return mineStone;
            }
        }
        static JobMode GatherClay
        {
            get
            {
                if (getClay == null)
                {
                    getClay = new JobMode("Clay", GetClayOnExecute);
                    getClay.LifeStages.Add(PopLifeStage.Child);
                    getClay.LifeStages.Add(PopLifeStage.Teen);
                    getClay.LifeStages.Add(PopLifeStage.YoungAdult);
                    getClay.LifeStages.Add(PopLifeStage.Adult);

                    getClay.Requirements += Requirements_Clay;

                }


                return getClay;
            }
        }
        static JobMode MineClay
        {
            get
            {
                if (mineClay == null)
                {
                    mineClay = new JobMode("Mine Clay", MineClayOnExecute);

                    mineClay.LifeStages.Add(PopLifeStage.Teen);
                    mineClay.LifeStages.Add(PopLifeStage.YoungAdult);
                    mineClay.LifeStages.Add(PopLifeStage.Adult);

                }


                return mineClay;
            }
        }
        static JobMode GatherWater
        {
            get
            {
                if (getWater == null)
                {
                    getWater = new JobMode("Water", GetWaterOnExecute);

                    getWater.LifeStages.Add(PopLifeStage.Child);
                    getWater.LifeStages.Add(PopLifeStage.Teen);
                    getWater.LifeStages.Add(PopLifeStage.YoungAdult);
                    getWater.LifeStages.Add(PopLifeStage.Adult);

                    getWater.Requirements += Requirements_FreshWater;
                }

                return getWater;
            }
        }
        static JobMode DigMud
        {
            get
            {
                if (getMud == null)
                {
                    getMud = new JobMode("Mud", GetMudOnExecute);
                    getMud.LifeStages.Add(PopLifeStage.Child);
                    getMud.LifeStages.Add(PopLifeStage.Teen);
                    getMud.LifeStages.Add(PopLifeStage.YoungAdult);
                    getMud.LifeStages.Add(PopLifeStage.Adult);

                    getMud.Requirements += Requirements_FreshWater;
                }


                return getMud;
            }
        }

        public static JobMode RetellStory
        {
            get
            {
                if(retellStory == null)
                {
                    retellStory = new JobMode("Retell", TellStoryOnExecute);
                }
                retellStory.LifeStages.Add(PopLifeStage.Teen);
                retellStory.LifeStages.Add(PopLifeStage.YoungAdult);
                retellStory.LifeStages.Add(PopLifeStage.Adult);
                return retellStory;
            }
        }
        public static JobMode NoMode
        {
            get
            {
                if (noMode == null)
                {
                    noMode = new JobMode("Nothing", null);
                    noMode.Requirements = (settlement) => { return false; };

                }

                return noMode;
            }
        }
        #endregion

        public static Job Gather
        {
            get
            {
                if(gather == null)
                {
                    gather = new Job("Gather");

                    gather.Modes.AddRange(new List<JobMode>() { NoMode, GatherFruit, GatherWood, GatherStone, GatherWater, });
                }
                gather.Modes.Skip(1).ToList().ForEach(m => m.Job = gather);
                return gather;
            }
        }

        public static Job Mine
        {
            get
            {
                if (mine == null)
                {
                    mine = new Job("Mine");

                    mine.Modes.AddRange(new List<JobMode> { NoMode, ChopTrees, MineStone, MineClay });

                    mine.Modes.Skip(1).ToList().ForEach(m => m.Job = mine);
                }

                return mine;
            }
        }

        public static Job StoryTell
        {
            get
            {
                if(storyTell == null)
                {
                    storyTell = new Job("Story Tell");

                    storyTell.Modes.AddRange(new List<JobMode> { NoMode, RetellStory });
                }
                
                storyTell.Modes.Skip(1).ToList().ForEach(m => m.Job = storyTell);
                return storyTell;
            }
        }


        public static void TellStoryOnExecute(Pop pop, Settlement settlement)
        {
            Story story = StoryFactory.MinorCourageousStory(pop, settlement);

            foreach (Pop listener in settlement.Population.Pops)
            {
                story.Execute(listener);
            }
        }

        

        public static void GetWoodOnExecute(Pop pop, Settlement settlement)
        {
            if(GetResourcesFromAnyHex(out float value, GameIndex.Wood, pop, settlement))
            {
                settlement.Stockpile.AddResource(GameIndex.Wood, value);
            }

        }
        public static void ChopTreesOnExecute(Pop pop, Settlement settlement)
        {
            settlement.Stockpile.AddResource(GameIndex.Wood, 10);
        }

        public static void GetFoodOnExecute(Pop pop, Settlement settlement)
        {
            if (GetResourcesFromAnyHex(out float value, GameIndex.Fruit, pop, settlement))
            {
                settlement.Stockpile.AddResource(GameIndex.Fruit, value);
            }
        }

        public static void GetStoneOnExecute(Pop pop, Settlement settlement)
        {
            if (GetResourcesFromAnyHex(out float value, GameIndex.Stone, pop, settlement))
            {
                settlement.Stockpile.AddResource(GameIndex.Stone, value);
            }
        }
        public static void MineStoneOnExecute(Pop pop, Settlement settlement)
        {
            settlement.Stockpile.AddResource(GameIndex.Stone, 3);
        }

        public static void GetClayOnExecute(Pop pop, Settlement settlement)
        {
            if (GetResourcesFromAnyHex(out float value, GameIndex.Clay, pop, settlement))
            {
                settlement.Stockpile.AddResource(GameIndex.Clay, value);
            }
        }
        public static void MineClayOnExecute(Pop pop, Settlement settlement)
        {
            settlement.Stockpile.AddResource(GameIndex.Clay, 3);
        }

        public static void GetWaterOnExecute(Pop pop, Settlement settlement)
        {
            if (GetResourcesFromAnyHex(out float value, GameIndex.Water, pop, settlement))
            {
                settlement.Stockpile.AddResource(GameIndex.Water, value);
            }
        }
        public static void GetMudOnExecute(Pop pop, Settlement settlement)
        {
            if (GetResourcesFromAnyHex(out float value, GameIndex.Mud, pop, settlement))
            {
                settlement.Stockpile.AddResource(GameIndex.Mud, value);
            }
        }

        static bool GetResourcesFromAnyHex(out float value, Resource resource, Pop pop, Settlement settlement)
        {
            value = 1;
            foreach (Hex hex in settlement.Territory)
            {
                if (hex.Economy.ResourcesAvailable.ContainsKey(resource))
                {
                    if (hex.Economy.ResourcesAvailable[resource] > 0)
                    {
                        hex.Economy.ResourcesAvailable[resource] -= value;
                        return true;
                    }
                }
            }

            return false;
        }

        static bool Requirements_Clay(Settlement settlement)
        {
            bool clayValue = false;
            foreach (Hex hex in settlement.Territory)
            {
                if (hex.Terrain.SoilType == SoilType.Clay)
                {
                    clayValue = true;
                    break;
                }
            }
            return clayValue;
        }
        static bool Requirements_FreshWater(Settlement settlement)
        {
            bool waterValue = false;

            foreach (Hex hex in settlement.Territory)
            {

            }

            return waterValue;
        }

        static bool Requirements_Wood(Settlement settlement)
        {
            bool woodValue = false;

            foreach (Hex hex in settlement.Territory)
            {
                if (hex.Economy.ResourcesAvailable[GameIndex.Wood] > 1)
                {
                    woodValue = true;
                    break;
                }
            }

            return woodValue;
        }
        static bool Requirements_Trees(Settlement settlement)
        {
            bool treeValue = false;

            foreach (Hex hex in settlement.Territory)
            {
                if (hex.Ecosystem.Trees.Values.Sum() > 0)
                {
                    treeValue = true;
                    break;
                }
            }

            return treeValue;
        }
    }
    
}

