using System.Collections.Generic;
using System.Linq;

namespace Assets.LevelEditor
{
    public static class EnumHelper
    {
        public static int GetCount<T>()
        {
            return System.Enum.GetNames(typeof(T)).Length;
        }

        public static List<T> GetAsList<T>()
        {
            return System.Enum.GetValues(typeof(T)).Cast<T>().ToList();
        }
    }

    public static class Enum
    {
        public static T Parse<T>(string name)
        {
            return (T) System.Enum.Parse(typeof (T), name);
        }
    }

    public enum MainTileType
    {
        Normal,
        Pillar,
        Wall,
        SpikeTrap,
        CurseTrigger
    }

    public enum ItemType
    {
        Spikes,
        PillarTrap,
        Hero,
        Boss,
        Box,
        VictoryExit
    }
}