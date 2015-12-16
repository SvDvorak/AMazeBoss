using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets
{
    public struct CompleteTileType
    {
        public MainTileType Main;
        public string Subtype;

        public CompleteTileType(MainTileType main, string subtype = "")
        {
            Main = main;
            Subtype = subtype.ToUpper();
        }

        private bool Equals(CompleteTileType other)
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
            return Equals((CompleteTileType)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int)Main * 397) ^ (Subtype != null ? Subtype.ToUpper().GetHashCode() : 0);
            }
        }

        public override string ToString()
        {
            return Main + " : " + Subtype;
        }
    }

    public class TileTypeTemplate
    {
        public CompleteTileType TileType;
        public List<GameObject> Templates;

        public TileTypeTemplate(CompleteTileType tileType, List<GameObject> templates)
        {
            TileType = tileType;
            Templates = templates;
        }
    }

    public static class TileLoader
    {
        private static readonly Dictionary<MainTileType, Dictionary<string, List<GameObject>>> TileTemplates =
            new Dictionary<MainTileType, Dictionary<string, List<GameObject>>>();

        static TileLoader()
        {
            LoadTileTypeTemplates();
        }

        private static void LoadTileTypeTemplates()
        {
            var tileTypes = TileTypeHelper.GetAsList();
            var allTiles = LoadTiles();

            foreach (var mainType in tileTypes)
            {
                var tileTypeGameObjects = allTiles
                    .Where(x => x.NameContains(mainType.ToString()))
                    .ToList();

                if (tileTypeGameObjects.Count == 0)
                {
                    Debug.LogWarning("No tiles available for " + mainType);
                }

                AddSubtypes(mainType, tileTypeGameObjects);
            }
        }

        private static void AddSubtypes(MainTileType mainType, List<GameObject> tileTypeGameObjects)
        {
            var subTypes = CreateSubTypeCollection();

            var templatesWithSubtypes = tileTypeGameObjects.Where(x => x.NameContains("_")).GroupBy(x => GetSubtype(x)).ToList();
            foreach (var subtype in templatesWithSubtypes)
            {
                subTypes.Add(subtype.Key.ToUpper(), subtype.ToList());
            }

            if (templatesWithSubtypes.Count == 0)
            {
                subTypes.Add("", tileTypeGameObjects);
            }

            TileTemplates.Add(mainType, subTypes);
        }

        private static Dictionary<string, List<GameObject>> CreateSubTypeCollection()
        {
            return new Dictionary<string, List<GameObject>>();
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

        public static TileTypeTemplate Retrieve(CompleteTileType completeType)
        {
            var mainTypeTemplates = TileTemplates[completeType.Main];

            List<GameObject> subTypeTemplates;
            if (completeType.Subtype != string.Empty)
            {
                subTypeTemplates = mainTypeTemplates[completeType.Subtype];
            }
            else
            {
                var defaultSubtypeTemplates = mainTypeTemplates.First();
                completeType.Subtype = defaultSubtypeTemplates.Key.ToUpper();
                subTypeTemplates = defaultSubtypeTemplates.Value;
            }

            return new TileTypeTemplate(completeType, subTypeTemplates);
        }
    }
}