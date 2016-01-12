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
        private class DescriptorSet
        {
            public DescriptorSet(string descriptorText, Func<Entity, bool> hasDescriptor, Action<Entity, string> setDescriptor, Func<Entity, string> getValue = null)
            {
                DescriptorText = descriptorText;
                HasDescriptor = hasDescriptor;
                SetDescriptor = setDescriptor;
            }

            public string DescriptorText { get; private set; }
            public Func<Entity, bool> HasDescriptor { get; private set; }
            public Action<Entity, string> SetDescriptor  { get; private set; }
            public bool HasValue { get { return GetValue != null; } }
            public Func<Entity, string> GetValue  { get; private set; }
        }

        private readonly List<DescriptorSet> _descriptorSets = new List<DescriptorSet>()
            {
                new DescriptorSet("ID", e => e.hasId, (e, val) => e.AddId(int.Parse(val)), e => e.id.Value.ToString()),
                new DescriptorSet("TILE", e => e.isTile, (e, val) => e.IsTile(true)),
                new DescriptorSet("WALKABLE", e => e.isWalkable, (e, val) => e.IsWalkable(true)),
                new DescriptorSet("SPIKETRAP", e => e.hasSpikeTrap, (e, val) => e.AddSpikeTrap(false)),
                new DescriptorSet("DYNAMIC", e => e.isDynamic, (e, val) => e.IsDynamic(true)),
                new DescriptorSet("ITEM", e => e.isItem, (e, val) => e.IsItem(true)),
                new DescriptorSet("SPIKES", e => e.isSpikes, (e, val) => e.IsSpikes(true)),
                new DescriptorSet("HERO", e => e.isHero, (e, val) => e.IsHero(true)),
                new DescriptorSet("BOSS", e => e.isBoss, (e, val) => e.IsBoss(true)),
                new DescriptorSet("CURSED", e => e.isCursed, (e, val) => e.IsCursed(true)),
                new DescriptorSet("HEALTH", e => e.hasHealth, (e, val) => e.AddHealth(int.Parse(val)), e => e.health.Value.ToString()),
            };

        public IEnumerable<string> ToDescriptors(Entity entity)
        {
            return _descriptorSets
                .Where(ds => ds.HasDescriptor(entity))
                .Select(ds => CreateDescriptorText(entity, ds))
                .ToList();
        }

        private static string CreateDescriptorText(Entity entity, DescriptorSet ds)
        {
            var descriptor = ds.DescriptorText;
            if (ds.HasValue)
            {
                descriptor += "(" + ds.GetValue(entity) + ")";
            }
            return descriptor;
        }

        public void FromDescriptors(string descriptors, Entity entity)
        {
            foreach (var descriptor in descriptors.Split(';'))
            {
                var correctDescriptorSet = _descriptorSets.Single(ds => descriptor.Contains(ds.DescriptorText));
                string value = null;
                if (descriptor.Contains("("))
                {
                    var startIndex = descriptor.IndexOf("(") + 1;
                    var endIndex = descriptor.IndexOf(")");
                    value = descriptor.Substring(startIndex, endIndex - startIndex);
                }
                correctDescriptorSet.SetDescriptor(entity, value);
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