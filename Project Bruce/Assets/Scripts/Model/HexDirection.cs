using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bruce
{
    public class HexDirection
    {
        static HexDirection()
        {
            NE = new HexDirection()
            {
                toString = "NE",
                Opposite = SW,
                Index = 0
            };
            E = new HexDirection()
            {
                toString = "E",
                Opposite = W,
                Index = 1
            };
            SE = new HexDirection()
            {
                toString = "SE",
                Opposite = NW,
                Index = 2
            };
            SW = new HexDirection()
            {
                toString = "SW",
                Opposite = NE,
                Index = 3
            };
            W = new HexDirection()
            {
                toString = "W",
                Opposite = E,
                Index = 4
            };
            NW = new HexDirection()
            {
                toString = "NW",
                Opposite = SE,
                Index = 5
            };
        }
        public static HexDirection NE;
        public static HexDirection E;
        public static HexDirection SE;
        public static HexDirection SW;
        public static HexDirection W;
        public static HexDirection NW;

        public HexDirection Opposite;
        public int Index;
        public string toString;

        public static HexDirection GetDirectionFromIndex(int index)
        {
            switch (index)
            {
                case 0:
                    return NE;
                case 1:
                    return E;
                case 2:
                    return SE;
                case 3:
                    return SW;
                case 4:
                    return W;
                case 5:
                    return NW;
                default:
                    return null;
            }
        }
    }
}