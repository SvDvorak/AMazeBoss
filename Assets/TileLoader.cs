using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets
{
    public static class TileLoader
    {
        private static readonly Dictionary<TileType, List<GameObject>> _tileTemplates = new Dictionary<TileType, List<GameObject>>();

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

                _tileTemplates.Add(tileType, tileTypeGameObjects);
            }
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

        public static GameObject Retrieve(TileType tileType)
        {
            var tiles = _tileTemplates[tileType];
            if (tiles.Count == 0)
            {
                throw new Exception("Cannot place tile, no template available for " + tileType);
            }

            return tiles[Random.Range(0, tiles.Count)];
        }
    }
}