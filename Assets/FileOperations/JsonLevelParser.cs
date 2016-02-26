using Assets.LevelEditor;
using Entitas;
using UnityEngine;

namespace Assets.FileOperations
{
    public class JsonLevelParser
    {
        public static string CreateLevelData(Pool pool)
        {
            var fileMap = LevelLoader.CreateLevelData(pool);
            return JsonUtility.ToJson(fileMap);
        }

        public static Level ReadLevelData(string json)
        {
            return JsonUtility
                .FromJson<Level>(json);
        }
    }

    public class PlayerPrefsLevelReader
    {
        public static void SaveLevel(string levelName, Level levelData, bool isNowUsed = true)
        {
            if(isNowUsed)
            {
                LastUsedLevelName = levelName;
            }

            var levelJson = JsonUtility.ToJson(levelData);
            PlayerPrefs.SetString(levelName, levelJson);
            AddOrUpdateLevelName(levelName);
        }

        public static void LoadLevel(string levelName)
        {
            LastUsedLevelName = levelName;
            Pools.game.SafeDeleteLevel();
            EditorSetup.Instance.Update();
            var levelData = GetLevel(levelName);
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

        public static Level GetLevel(string levelName)
        {
            return JsonUtility.FromJson<Level>(PlayerPrefs.GetString(levelName));
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

        public static string LastUsedLevelName
        {
            get { return PlayerPrefs.GetString("LastUsedLevel"); }
            set { PlayerPrefs.SetString("LastUsedLevel", value);}
        }
    }
}