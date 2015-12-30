using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Entitas;
using UnityEngine;

namespace Assets.FileOperations
{
    public class DescriptorResolver
    {
        public IEnumerable<string> ToDescriptors(Entity entity)
        {
            var descriptors = new List<string>();
            if (entity.isTile)
            {
                descriptors.Add("TILE");
            }
            if (entity.isWalkable)
            {
                descriptors.Add("WALKABLE");
            }
            if (entity.isItem)
            {
                descriptors.Add("ITEM");
            }
            if (entity.isHero)
            {
                descriptors.Add("HERO");
            }
            if (entity.isBoss)
            {
                descriptors.Add("BOSS");
            }

            return descriptors;
        }

        private readonly Dictionary<string, Action<Entity>> _fromDescriptors = new Dictionary<string, Action<Entity>>()
            {
                { "TILE", (entity) => entity.IsTile(true) },
                { "WALKABLE", (entity) => entity.IsWalkable(true) },
                { "ITEM", (entity) => entity.IsItem(true) },
                { "HERO", (entity) => entity.IsHero(true) },
                { "BOSS", (entity) => entity.IsBoss(true) }
            };

        public void FromDescriptors(string descriptors, Entity entity)
        {
            foreach (var descriptor in descriptors.Split(';'))
            {
                _fromDescriptors[descriptor](entity);
            }
        }
    }

    public class FileOperations
    {
        private static readonly DescriptorResolver DescriptorResolver = new DescriptorResolver();

        public static void Save(string path)
        {
            var editorObjects = Pools.pool.GetEntities(Matcher.AnyOf(Matcher.Tile, Matcher.Item));
            var fileObjects = new MapObjects(editorObjects
                .Select(x => CreateFileObject(x))
                .ToList());
            var json = JsonUtility.ToJson(fileObjects);
            var streamWriter = new StreamWriter(path, false);
            streamWriter.Write(json);
            streamWriter.Close();
        }

        private static FileObject CreateFileObject(Entity entity)
        {
            var pos = entity.position.Value;
            return new FileObject(entity.maintype.Value, entity.subtype.Value, pos.X, pos.Z, entity.rotation.Value, DescriptorResolver.ToDescriptors(entity));
        }

        public static void Load(string path)
        {
            var streamReader = new StreamReader(path);
            var json = streamReader.ReadToEnd();
            streamReader.Close();

            var pool = Pools.pool;

            JsonUtility
                .FromJson<MapObjects>(json)
                .Tiles
                .ForEach(tile => CreateEntity(pool, tile));
        }

        private static void CreateEntity(Pool pool, FileObject mapObject)
        {
            var entity = pool.CreateEntity()
                .AddMaintype(mapObject.MainType)
                .AddSubtype(mapObject.Subtype)
                .AddPosition(new TilePos(mapObject.X, mapObject.Z))
                .AddRotation(mapObject.Rotation);

            DescriptorResolver.FromDescriptors(mapObject.Descriptors, entity);
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