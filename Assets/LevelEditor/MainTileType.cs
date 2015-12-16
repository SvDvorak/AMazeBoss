using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets
{
    public static class TileTypeHelper
    {
        public static int GetCount()
        {
            return Enum.GetNames(typeof(MainTileType)).Length;
        }

        public static List<MainTileType> GetAsList()
        {
            return Enum.GetValues(typeof(MainTileType)).Cast<MainTileType>().ToList();
        }
    }

    public enum MainTileType
    {
        Normal,
        Pillar,
        Wall,
        Spike,
        Hero,
        Boss
    }
}