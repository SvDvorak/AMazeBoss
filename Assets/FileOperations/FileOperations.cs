using System;
using System.IO;
using Assets.LevelEditor;
using UnityEngine;

namespace Assets.FileOperations
{
    public class FileOperations
    {
        public static void Save(string path)
        {
            var levelData = LevelParser.CreateLevelData(Pools.game);
            var streamWriter = new StreamWriter(path, false);
            streamWriter.Write(levelData);
            streamWriter.Close();

            SetLastUsedPath(path);
        }

        public static void Load(string path)
        {
            try
            {
                var streamReader = new StreamReader(path);
                var json = streamReader.ReadToEnd();
                streamReader.Close();

                LevelParser.ReadLevelData(json, Pools.game);
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

        private static void SetLastUsedPath(string path)
        {
            PlaySetup.LevelPath = path;
            PlayerPrefs.SetString("LastEditorLevel", path);
            Events.instance.Raise(new DefaultPathChanged(path));
        }
    }
}