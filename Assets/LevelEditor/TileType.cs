using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets
{
    public static class TileTypeHelper
    {
        public static int GetCount()
        {
            return Enum.GetNames(typeof(TileType)).Length;
        }

        public static List<TileType> GetAsList()
        {
            return Enum.GetValues(typeof(TileType)).Cast<TileType>().ToList();
        }
    }

    public enum TileType
    {
        Normal,
        Pillar,
        Wall,
        Spike
    }
}