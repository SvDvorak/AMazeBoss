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
    public MainTileType MainType;
    public string Subtype;
    public int X;
    public int Z;

    public FileTile(MainTileType mainType, string subtype, int x, int z)
    {
        MainType = mainType;
        Subtype = subtype;
        X = x;
        Z = z;
    }
}

public class UIInteractions : MonoBehaviour
{
    public void Save(string path)
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
        return new FileTile(tileType.Main, tileType.Subtype, tile.Key.X, tile.Key.Z);
    }

    public void Load(string path)
    {
        var streamReader = new StreamReader(path);
        var json = streamReader.ReadToEnd();
        streamReader.Close();

        var tiles = JsonUtility
            .FromJson<FileTiles>(json)
            .Tiles
            .ToDictionary(
                fileTile => new TilePos(fileTile.X, fileTile.Z),
                fileTile => new CompleteTileType(fileTile.MainType, fileTile.Subtype));

        RoomInfo.SetAllTiles(tiles);
    }

    public void Clear()
    {
        RoomInfo.ClearTiles();
    }
}