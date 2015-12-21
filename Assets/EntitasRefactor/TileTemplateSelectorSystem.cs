using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;
using Assets;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.EntitasRefactor
{
    public class TileTemplateSelectorSystem : IReactiveSystem, ISetPool
    {
        private Pool _pool;

        public TriggerOnEvent trigger { get { return Matcher.Tile.OnEntityAdded(); } }

        public void SetPool(Pool pool)
        {
            _pool = pool;
        }

        public void Execute(List<Entity> entities)
        {
            foreach (var entity in entities)
            {
                entity.ReplaceResource(RetrieveTileTemplateName(entity));
            }
        }

        private string RetrieveTileTemplateName(Entity entity)
        {
            List<string> templateNames;
            if (entity.hasSubtype)
            {
                templateNames = _pool.tileTemplates.Value.Retrieve(entity.tile.Type, entity.subtype.Value);
            }
            else
            {
                var selectedSubtype = _pool.tileTemplates.Value.Retrieve(entity.tile.Type);
                entity.AddSubtype(selectedSubtype.Item1);
                templateNames = selectedSubtype.Item2;
            }

            return templateNames[Random.Range(0, templateNames.Count)];
        }
    }

    public class TileTemplateLoaderSystem : IInitializeSystem, ISetPool
    {
        private readonly string _tilesPath = "Tiles/";
        private Pool _pool;

        public void SetPool(Pool pool)
        {
            _pool = pool;
        }

        public void Initialize()
        {
            LoadTileTypeTemplates();
        }

        private void LoadTileTypeTemplates()
        {
            var tileTypes = TileTypeHelper.GetAsList();
            var allTiles = LoadTiles();
            var templateNames = new TemplateNames();

            foreach (var mainType in tileTypes)
            {
                var tileTypeGameObjects = allTiles
                    .Where(x => x.NameContains(mainType.ToString()))
                    .ToList();

                if (tileTypeGameObjects.Count == 0)
                {
                    Debug.LogWarning("No tiles available for " + mainType);
                }

                templateNames.Add(mainType, GetSubtypes(tileTypeGameObjects));
            }

            _pool.SetTileTemplates(templateNames);
        }

        private SubtemplateNames GetSubtypes(List<GameObject> tileTypeGameObjects)
        {
            var subTypes = new SubtemplateNames();

            var templatesWithSubtypes = tileTypeGameObjects.Where(x => x.NameContains("_")).GroupBy(x => GetSubtype(x)).ToList();
            foreach (var subtype in templatesWithSubtypes)
            {
                subTypes.Add(subtype.Key.ToUpper(), subtype.Select(x => _tilesPath + x.name).ToList());
            }

            if (templatesWithSubtypes.Count == 0)
            {
                subTypes.Add("", tileTypeGameObjects.Select(x => _tilesPath + x.name).ToList());
            }

            return subTypes;
        }

        private static string GetSubtype(GameObject x)
        {
            var subTypeIndex = x.name.IndexOf("_");
            return x.name.Substring(subTypeIndex + 1);
        }

        private GameObject[] LoadTiles()
        {
            try
            {
                return Resources.LoadAll<GameObject>(_tilesPath);
            }
            catch (Exception)
            {
                throw new Exception("Cannot load tiles from Resources/Tiles");
            }
        }
    }
}
