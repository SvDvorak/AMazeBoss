using System;
using System.IO;
using System.Linq;
using Assets.LevelEditor;
using Entitas;
using UnityEngine;

namespace Assets.FileOperations
{
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

            SetLastUsedPath(path);
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

            try
            {
                JsonUtility
                    .FromJson<MapObjects>(json)
                    .Tiles
                    .ForEach(tile => CreateEntity(pool, tile));

                SetLastUsedPath(path);
            }
            catch (Exception ex)
            {
                throw new LevelParseException(path, ex);
            }
        }

        public class LevelParseException : Exception
        {
            public LevelParseException(string filePath, Exception inner) : base("Unable to parse " + filePath, inner) { }
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
            if (PlaySetup.FromEditor)
            {
                return PlaySetup.LevelPath;
            }
            if (PlayerPrefs.HasKey("LastEditorLevel"))
            {
                return PlayerPrefs.GetString("LastEditorLevel");
            }

            return null;
        }

        public static void SetLastUsedPath(string path)
        {
            PlaySetup.LevelPath = path;
            PlayerPrefs.SetString("LastEditorLevel", path);
            Events.instance.Raise(new DefaultPathChanged(path));
        }
    }
}