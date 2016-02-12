using System.Collections.Generic;
using System.Linq;
using Assets.LevelEditor;
using Entitas;
using UnityEngine;

namespace Assets.FileOperations
{
    public class LevelLoader
    {
        private static readonly DescriptorResolver DescriptorResolver = new DescriptorResolver();

        public static Level CreateLevelData(Pool pool)
        {
            var mapObjects = pool.GetEntities(Matcher.AnyOf(GameMatcher.Tile, GameMatcher.Item));
            var fileMap = new Level(
                CreateFileCamera(pool),
                mapObjects
                    .Select(x => CreateFileMapObject(x))
                    .ToList());

            return fileMap;
        }

        private static LevelCamera CreateFileCamera(Pool pool)
        {
            var cameraFocus = pool.GetEntities(GameMatcher.Camera).SingleEntity().savedFocusPoint;
            return new LevelCamera(cameraFocus.Position.x, cameraFocus.Position.z);
        }

        private static LevelObject CreateFileMapObject(Entity entity)
        {
            var pos = entity.position.Value;
            return new LevelObject(entity.maintype.Value, entity.subtype.Value, pos.X, pos.Z, entity.rotation.Value,
                DescriptorResolver.ToDescriptors(entity));
        }

        public static void ReadLevelData(Level level, Pool pool)
        {
            level
                .Tiles
                .ForEach(tile => CreateEntity(pool, tile));

            var alreadyHasCamera = pool.GetEntities(GameMatcher.Camera).Any();
            if (!alreadyHasCamera)
            {
                CreateEntity(pool, level.Camera);
            }
        }

        private static void CreateEntity(Pool pool, LevelCamera camera)
        {
            pool.GetEntities(GameMatcher.Camera).SingleEntity()
                .ReplaceFocusPoint(new Vector3(camera.FocusX, 0, camera.FocusZ))
                .ReplaceSavedFocusPoint(new Vector3(camera.FocusX, 0, camera.FocusZ));
        }

        private static void CreateEntity(Pool pool, LevelObject mapGameObject)
        {
            var entity = pool
                .CreateEntity()
                .AddMaintype(mapGameObject.MainType)
                .AddSubtype(mapGameObject.Subtype)
                .AddPosition(new TilePos(mapGameObject.X, mapGameObject.Z))
                .AddRotation(mapGameObject.Rotation);

            DescriptorResolver.FromDescriptors(mapGameObject.Descriptors, entity);
        }
    }

    public class JsonLevelParser
    {
        public static string CreateLevelData(Pool pool)
        {
            var fileMap = LevelLoader.CreateLevelData(pool);
            return JsonUtility.ToJson(fileMap);
        }

        public static void ReadLevelData(string json, Pool pool)
        {
            var fileMap = JsonUtility
                .FromJson<Level>(json);

            LevelLoader.ReadLevelData(fileMap, pool);
        }
    }

    public class PlayerPrefsLevelReader
    {
        public static void SaveLevel(string levelName, Level levelData)
        {
            var levelJson = JsonUtility.ToJson(levelData);
            PlayerPrefs.SetString(levelName, levelJson);
            AddOrUpdateLevelName(levelName);
        }

        public static void LoadLevel(string levelName)
        {
            Pools.game.Clear(Matcher.AnyOf(GameMatcher.Tile, GameMatcher.Item));
            EditorSetup.Instance.Update();
            var levelData = JsonUtility.FromJson<Level>(PlayerPrefs.GetString(levelName));
            LevelLoader.ReadLevelData(levelData, Pools.game);
        }

        public static void Delete(string levelName)
        {
            PlayerPrefs.DeleteKey(levelName);
            RemoveLevelName(levelName);
        }

        private static void AddOrUpdateLevelName(string savedName)
        {
            var levelsInfo = GetLevelsInfo();
            levelsInfo.AddOrUpdate(savedName);
            PlayerPrefs.SetString("LevelsInfo", JsonUtility.ToJson(levelsInfo));
        }

        private static void RemoveLevelName(string deletedName)
        {
            var levelsInfo = GetLevelsInfo();
            levelsInfo.Remove(deletedName);
            PlayerPrefs.SetString("LevelsInfo", JsonUtility.ToJson(levelsInfo));
        }

        public static LevelsInfo GetLevelsInfo()
        {
            var currentLevels = PlayerPrefs.GetString("LevelsInfo");
            if (string.IsNullOrEmpty(currentLevels))
            {
                return new LevelsInfo();
            }

            return JsonUtility.FromJson<LevelsInfo>(currentLevels);
        }
    }
}