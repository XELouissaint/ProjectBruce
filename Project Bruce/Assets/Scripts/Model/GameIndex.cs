using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bruce
{
    public static class GameIndex
    {
        public static int PopIDCounter;

        public static Cultivar None = new Cultivar("none", 0f);

        public static string Commerce = "commerce";

        public static string OakTree = "oak tree";
        public static string AppleTree = "apple tree";

        public static Cultivar Apple = new Cultivar("apple", .1f);

        public static Wood Wood = new Wood("wood", 1f);
        public static Water Water = new Water("water");

        public static int WorkingAge = 8;

        public static float baseWoodGatherRate = .1f;
    }

    public interface Resource
    {
        string name { get; set; }
    }
    public class Cultivar : Resource
    {
        public Cultivar(string name, float foodValue)
        {
            this.name = name;
            this.foodValue = foodValue;
        }
        public string name { get; set; }
        public float foodValue;
    }

    public class Wood : Resource
    {
        public Wood(string name, float quality)
        {
            this.name = name;
            this.quality = quality;
        }
        public string name { get; set; }
        public float quality;
    }

    public class Water : Resource
    {
        public Water(string name)
        {
            this.name = name;
        }

        public string name { get; set; }
    }
}
