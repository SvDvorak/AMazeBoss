using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Entitas;
using UnityEngine;

namespace Assets.FileOperations
{
    public class FileOperations
    {
        public static void Save(string path)
        {
            var editorTiles = Pools.pool.GetEntities(Matcher.Tile);
            var fileTiles = new FileTiles(editorTiles
                .Select(x => CreateFileTile(x))
                .ToList());
            var json = JsonUtility.ToJson(fileTiles);
            var streamWriter = new StreamWriter(path, false);
            streamWriter.Write(json);
            streamWriter.Close();
        }

        private static FileTile CreateFileTile(Entity entity)
        {
            var pos = entity.position.Value;
            var subtype = entity.hasSubtype ? entity.subtype.Value : "";
            return new FileTile(entity.maintype.Value, subtype, pos.X, pos.Z, entity.rotation.Value);
        }

        public static void Load(string path)
        {
            var streamReader = new StreamReader(path);
            var json = streamReader.ReadToEnd();
            streamReader.Close();

            var pool = Pools.pool;

            JsonUtility
                .FromJson<FileTiles>(json)
                .Tiles
                .ForEach(tile => CreateEntity(pool, tile));
        }

        private static void CreateEntity(Pool pool, FileTile tile)
        {
            var entity = pool.CreateEntity()
                .AddTile(Enum.Parse<MainTileType>(tile.MainType))
                .AddMaintype(tile.MainType)
                .AddPosition(new TilePos(tile.X, tile.Z))
                .AddRotation(tile.Rotation);

            if (tile.Subtype != "")
            {
                entity.AddSubtype(tile.Subtype);
            }
        }

        public static string GetLastUsedPath()
        {
            if (LoadLevel.EditorLevelPath != "")
            {
                return LoadLevel.EditorLevelPath;
            }
            if (PlayerPrefs.HasKey("LastEditorLevel"))
            {
                return PlayerPrefs.GetString("LastEditorLevel");
            }

            return null;
        }
    }
}