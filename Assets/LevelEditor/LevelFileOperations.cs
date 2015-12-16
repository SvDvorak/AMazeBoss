using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Assets;
using UnityEngine;

[Serializable]
public class FileTiles
{
    public List<FileTile> Tiles;

    public FileTiles(List<FileTile> tiles)
    {
        Tiles = tiles;
    }
}

[Serializable]
public class FileTile
{
    public MainTileType Type;
    public int X;
    public int Z;

    public FileTile(MainTileType type, int x, int z)
    {
        Type = type;
        X = x;
        Z = z;
    }
}

public class LevelFileOperations : MonoBehaviour
{
    public void Save(string path)
    {
        var tileInfos = RoomInfo.GetAllTiles();
        var tiles = new FileTiles(tileInfos
            .Select(tile => new FileTile(tile.Value.TileType.Main, tile.Key.X, tile.Key.Z))
            .ToList());
        var json = JsonUtility.ToJson(tiles);
        var streamWriter = new StreamWriter(path, false);
        streamWriter.Write(json);
        streamWriter.Close();
    }

    public void Load(string path)
    {
        var streamReader = new StreamReader(path);
        var json = streamReader.ReadToEnd();
        streamReader.Close();

        var tiles = JsonUtility
            .FromJson<FileTiles>(json)
            .Tiles
            .ToDictionary(fileTile => new TilePos(fileTile.X, fileTile.Z), fileTile => fileTile.Type);

        RoomInfo.SetAllTiles(tiles);
    }
}
