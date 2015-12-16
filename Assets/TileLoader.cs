using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets
{
    public struct TileTypeA
    {
        public TileType Main;
        public string Subtype;

        public TileTypeA(TileType main, string subtype = "")
        {
            Main = main;
            Subtype = subtype;
        }

        private bool Equals(TileTypeA other)
        {
            return Main == other.Main && string.Equals(Subtype.ToUpper(), other.Subtype.ToUpper());
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            return Equals((TileTypeA) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int) Main*397) ^ (Subtype != null ? Subtype.ToUpper().GetHashCode() : 0);
            }
        }

        public override string ToString()
        {
            return Main + " : " + Subtype;
        }
    }

    public class TileTypeTemplate
    {
        public TileTypeA TileType;
        public List<GameObject> Templates;

        public TileTypeTemplate(TileTypeA tileType, List<GameObject> templates)
        {
            TileType = tileType;
            Templates = templates;
        }
    }

    public static class TileLoader
    {
        private static readonly Dictionary<TileTypeA, List<GameObject>> TileTemplates = new Dictionary<TileTypeA, List<GameObject>>();

        static TileLoader()
        {
            LoadTileTypeTemplates();
        }

        private static void LoadTileTypeTemplates()
        {
            var tileTypes = TileTypeHelper.GetAsList();
            var allTiles = LoadTiles();

            foreach (var tileType in tileTypes)
            {
                var tileTypeGameObjects = allTiles
                    .Where(x => x.NameContains(tileType.ToString()))
                    .ToList();

                if (tileTypeGameObjects.Count == 0)
                {
                    Debug.LogWarning("No tiles available for " + tileType);
                }

                TileTemplates.Add(new TileTypeA(tileType), tileTypeGameObjects.ToList());
                AddSubtypes(tileType, tileTypeGameObjects);
            }
        }

        private static void AddSubtypes(TileType tileType, List<GameObject> tileTypeGameObjects)
        {
            var templatesWithSubtypes = tileTypeGameObjects.Where(x => x.NameContains("_")).GroupBy(x => GetSubtype(x)).ToList();
            foreach (var subtype in templatesWithSubtypes)
            {
                TileTemplates.Add(new TileTypeA(tileType, subtype.Key), subtype.ToList());
            }
        }

        private static string GetSubtype(GameObject x)
        {
            var subTypeIndex = x.name.IndexOf("_");
            return x.name.Substring(subTypeIndex + 1);
        }

        private static GameObject[] LoadTiles()
        {
            try
            {
                return Resources.LoadAll<GameObject>("Tiles");
            }
            catch (Exception)
            {
                throw new Exception("Cannot load tiles from Resources/Tiles");
            }
        }

        public static TileTypeTemplate Retrieve(TileTypeA type)
        {
            return new TileTypeTemplate(type, TileTemplates[type]);
        }
    }
}