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
            var levelData = JsonLevelParser.CreateLevelData(Pools.game);
            var streamWriter = new StreamWriter(path + ".json", false);
            streamWriter.Write(levelData);
            streamWriter.Close();
        }

        public static void Load(string path)
        {
            try
            {
                var streamReader = new StreamReader(path);
                var json = streamReader.ReadToEnd();
                streamReader.Close();

                JsonLevelParser.ReadLevelData(json, Pools.game);
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
    }
}