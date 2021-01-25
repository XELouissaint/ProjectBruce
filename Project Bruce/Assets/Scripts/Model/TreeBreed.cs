using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bruce
{
    public class TreeBreed
    {
        public TreeBreed(Resource cultivar, string name, int maxSize, int maxResources)
        {
            this.cultivar = cultivar;
            this.Name = name;
            this.maxSize = maxSize;
            this.maxResources = maxResources;
        }
        public Resource cultivar;
        public string Name;
        public int maxSize;
        public float maxResources;

        static TreeBreed oak;
        static TreeBreed apple;

        public static TreeBreed Oak
        {
            get
            {
                if(oak == null)
                {
                    oak = new TreeBreed(GameIndex.PlantFibers, GameIndex.OakTree, 8, 10);
                }

                return oak;
            }
        }

        public static TreeBreed Apple
        {
            get
            {
                if (apple == null)
                {
                    apple = new TreeBreed(GameIndex.Fruit, GameIndex.AppleTree, 8, 42);
                }

                return apple;
            }
        }

    }
}