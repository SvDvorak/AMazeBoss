using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Assets.FileOperations
{
    public class FileOperations
    {
        public static void Save(string path)
        {
            var tileInfos = RoomInfo.GetAllTiles();
            var tiles = new FileTiles(tileInfos
                .Select(x => CreateFileTile(x))
                .ToList());
            var json = JsonUtility.ToJson(tiles);
            var streamWriter = new StreamWriter(path, false);
            streamWriter.Write(json);
            streamWriter.Close();
        }

        private static FileTile CreateFileTile(KeyValuePair<TilePos, TileInfo> tile)
        {
            var tileType = tile.Value.TileType;
            return new FileTile(tileType.Main, tileType.Subtype, tile.Key.X, tile.Key.Z, tile.Value.Rotation);
        }

        public static void Load(string path)
        {
            var streamReader = new StreamReader(path);
            var json = streamReader.ReadToEnd();
            streamReader.Close();

            var tiles = JsonUtility
                .FromJson<FileTiles>(json)
                .Tiles
                .ToDictionary(
                    fileTile => new TilePos(fileTile.X, fileTile.Z),
                    fileTile => new TileInfo(
                        new CompleteTileType(fileTile.MainType, fileTile.Subtype), null, fileTile.Rotation));

            RoomInfo.SetAllTiles(tiles);
        }
    }
}