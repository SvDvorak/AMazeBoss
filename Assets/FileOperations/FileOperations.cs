using System;
using System.IO;

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

        public static Level Load(string path)
        {
            try
            {
                var streamReader = new StreamReader(path + ".json");
                var json = streamReader.ReadToEnd();
                streamReader.Close();

                return JsonLevelParser.ReadLevelData(json);
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