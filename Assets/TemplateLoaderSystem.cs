using System;
using System.Collections.Generic;
using System.Linq;
using Assets.LevelEditor;
using Entitas;
using UnityEngine;

namespace Assets
{
    [Game]
    public class TemplateLoaderSystem : IInitializeSystem, ISetPool
    {
        private Pool _pool;

        public void SetPool(Pool pool)
        {
            _pool = pool;
        }

        public void Initialize()
        {
            var templateNames = new TemplateNames();
            LoadTileTypeTemplates(templateNames, "Tiles/", EnumHelper.GetAsList<MainTileType>().Select(x => x.ToString()));
            LoadTileTypeTemplates(templateNames, "Items/", EnumHelper.GetAsList<ItemType>().Select(x => x.ToString()));
            LoadTileTypeTemplates(templateNames, "Areas/", EnumHelper.GetAsList<AreaType>().Select(x => x.ToString()));
            _pool.SetTileTemplates(templateNames);
        }

        private void LoadTileTypeTemplates(TemplateNames templateNames, string typeClass, IEnumerable<string> mainTypes)
        {
            var allTiles = LoadTiles(typeClass);

            foreach (var mainType in mainTypes)
            {
                var tileTypeGameObjects = allTiles
                    .Where(x => x.NameContains(mainType.ToString()))
                    .ToList();

                if (tileTypeGameObjects.Count == 0)
                {
                    Debug.LogWarning("No tiles available for " + mainType);
                }

                templateNames.Add(mainType.ToUpper(), GetSubtypes(tileTypeGameObjects, typeClass));
            }
        }

        private SubtemplateNames GetSubtypes(List<GameObject> tileTypeGameObjects, string type)
        {
            var subTypes = new SubtemplateNames();

            var templatesWithSubtypes =
                tileTypeGameObjects.Where(x => x.NameContains("_")).GroupBy(x => GetSubtype(x)).ToList();
            foreach (var subtype in templatesWithSubtypes)
            {
                subTypes.Add(subtype.Key.ToUpper(), subtype.Select(x => type + x.name).ToList());
            }

            if (templatesWithSubtypes.Count == 0)
            {
                subTypes.Add("", tileTypeGameObjects.Select(x => type + x.name).ToList());
            }

            return subTypes;
        }

        private static string GetSubtype(GameObject x)
        {
            var subTypeIndex = x.name.IndexOf("_");
            return x.name.Substring(subTypeIndex + 1);
        }

        private GameObject[] LoadTiles(string path)
        {
            try
            {
                return Resources.LoadAll<GameObject>(path);
            }
            catch (Exception)
            {
                throw new Exception("Cannot load tiles from Resources/Tiles");
            }
        }
    }
}