using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bruce
{
    public static class GameIndex
    {
        public static int PopIDCounter;


        public static string Commerce = "commerce";

        public static string OakTree = "oak tree";
        public static string AppleTree = "apple tree";
        public static Cultivar None = new Cultivar("none", 0f);
        public static Cultivar Fruit = new Cultivar("fruit", .1f);
        public static Wood Wood = new Wood("wood", 1f);
        public static Water Water = new Water("water");
        public static Stone Stone = new Stone("stone");
        public static Clay Clay = new Clay("clay");

        public static int WorkingAge = 8;

        public static float baseWoodGatherRate = .1f;
    }

    public interface Resource
    {
        string name { get; set; }
    }

    public interface Stat
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

    public class Stone : Resource
    {
        public Stone (string name)
        {
            this.name = name;
        }

        public string name { get; set; }
    }
    public class Clay : Resource
    {
        public Clay(string name)
        {
            this.name = name;
        }

        public string name { get; set; }
    }
    public class Grass
    {
        public Grass(string name, float growthValue = 1f)
        {
            this.name = name;
            this.growthValue = growthValue;
        }
        float growthValue;
        public string name { get; set; }
    }

    public class Strength : Stat
    {
        public Strength(string name)
        {
            this.name = name;
        }

        public string name { get; set; }
    }

    #region LimitList
    public class LimitList<T> : IEnumerable<T> where T : Pop
    {
        public LimitList(int length)
        {

            list = new List<T>();

            SetLength(length);
        }
        int length;
        List<T> list;

        public void SetLength (int paramValue)
        {
            if(list.Count > paramValue)
            {
                while (list.Count > paramValue)
                {
                    list.RemoveAt(list.Count - 1);
                }
            }

            length = paramValue;
        }
        public T this[int index]
        {
            get
            {
                return this.list[index];
            }
            set
            {
                this.list.Insert(index, value);
            }
        }
        public int Count
        {
            get
            {
                return list.Count;
            }
        }
        public bool Add(T pop)
        {
            if (list.Count >= length)
            {
                return false;
            }

            list.Add(pop);
            return true;
        }

        public bool Remove(T pop)
        {
            return list.Remove(pop);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
    #endregion
}
