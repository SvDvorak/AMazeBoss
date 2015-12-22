using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets
{
    public static class EnumHelper
    {
        public static int GetCount<T>()
        {
            return Enum.GetNames(typeof(T)).Length;
        }

        public static List<T> GetAsList<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>().ToList();
        }
    }

    public enum MainTileType
    {
        Normal,
        Pillar,
        Wall,
        Spike,
    }

    public enum ItemType
    {
        Hero,
        Boss
    }
}