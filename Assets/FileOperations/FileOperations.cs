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
            var pool = Pools.pool;
            var mapObjects = pool.GetEntities(Matcher.AnyOf(Matcher.Tile, Matcher.Item));
            var fileMap = new FileMap(
                CreateFileCamera(pool),
                mapObjects
                    .Select(x => CreateFileMapObject(x))
                    .ToList());
            var json = JsonUtility.ToJson(fileMap);
            var streamWriter = new StreamWriter(path, false);
            streamWriter.Write(json);
            streamWriter.Close();

            SetLastUsedPath(path);
        }

        private static FileCamera CreateFileCamera(Pool pool)
        {
            var cameraFocus = pool.GetEntities(Matcher.Camera).SingleEntity().savedFocusPoint;
            return new FileCamera(cameraFocus.Position.x, cameraFocus.Position.z);
        }

        private static FileMapObject CreateFileMapObject(Entity entity)
        {
            var pos = entity.position.Value;
            return new FileMapObject(entity.maintype.Value, entity.subtype.Value, pos.X, pos.Z, entity.rotation.Value, DescriptorResolver.ToDescriptors(entity));
        }

        public static void Load(string path)
        {
            var streamReader = new StreamReader(path);
            var json = streamReader.ReadToEnd();
            streamReader.Close();

            var pool = Pools.pool;

            try
            {
                var fileLevelObjects = JsonUtility
                    .FromJson<FileMap>(json);
                CreateEntity(pool, fileLevelObjects.Camera);
                fileLevelObjects
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
            public LevelParseException(string filePath, Exception inner) : base("Unable to parse " + filePath, inner)
            {
            }
        }

        private static void CreateEntity(Pool pool, FileCamera camera)
        {
            pool.CreateEntity()
                .AddResource("Camera")
                .AddFocusPoint(new Vector3(camera.FocusX, 0, camera.FocusZ))
                .AddSavedFocusPoint(new Vector3(camera.FocusX, 0, camera.FocusZ))
                .AddRotation(0);
        }

        private static void CreateEntity(Pool pool, FileMapObject mapGameObject)
        {
            var entity = pool
                .CreateEntity()
                .AddMaintype(mapGameObject.MainType)
                .AddSubtype(mapGameObject.Subtype)
                .AddPosition(new TilePos(mapGameObject.X, mapGameObject.Z))
                .AddRotation(mapGameObject.Rotation);

            DescriptorResolver.FromDescriptors(mapGameObject.Descriptors, entity);
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