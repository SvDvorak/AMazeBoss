using System;
using System.Collections.Generic;
using System.Linq;
using Assets;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class TileInfo
{
    public CompleteTileType TileType { get; private set; }
    public GameObject Tile { get; private set; }
    public int Rotation { get; private set; }

    public TileInfo(CompleteTileType tileType, GameObject tile, int rotation)
    {
        TileType = tileType;
        Tile = tile;
        Rotation = rotation;
    }
}


public static class RoomInfo
{
    private static GameObject _tilesRoot;
    private static readonly Dictionary<TilePos, TileInfo> Tiles = new Dictionary<TilePos, TileInfo>();
    private static readonly HashSet<MainTileType> WalkableTiles = new HashSet<MainTileType>()
    {
        MainTileType.Normal,
        MainTileType.Spike,
        MainTileType.Hero,
        MainTileType.Boss
    };

    private static bool _pauseEvents;

    static RoomInfo()
    {
        Events.instance.AddListener<LoadingScene>(x => ClearTiles());
    }

    public static void Init()
    {
        _tilesRoot = new GameObject("Tiles");
    }

    public static bool HasAnyTileAt(TilePos pos)
    {
        return Tiles.ContainsKey(pos);
    }

    public static bool HasTileAt(TilePos pos, MainTileType type)
    {
        return Tiles.ContainsKey(pos) && Tiles[pos].TileType.Main == type;
    }

    public static bool CanMoveTo(TilePos pos)
    {
        return HasAnyTileAt(pos) && WalkableTiles.Contains(Tiles[pos].TileType.Main);
    }

    public static void AddOrReplaceTile(TilePos tilePos, MainTileType type)
    {
        AddOrReplaceTile(tilePos, new CompleteTileType(type));
    }

    public static void AddOrReplaceTile(TilePos tilePos, CompleteTileType type, int? preferredRotation = null)
    {
        RemoveTile(tilePos);

        var tileTemplate = TileLoader.Retrieve(type);

        var randomTemplate = tileTemplate.Templates[Random.Range(0, tileTemplate.Templates.Count)];
        var rotation = preferredRotation ?? Random.Range(0, 4);
        var tile = CreateParented(tilePos, randomTemplate, tileTemplate.Bottom, rotation);
        Tiles.Add(tilePos, new TileInfo(tileTemplate.TileType, tile, rotation));

        if(!_pauseEvents)
        {
            Events.instance.Raise(new TileAdded(tilePos, type.Main, tile));
        }
    }

    private static GameObject CreateParented(TilePos tilePos, GameObject tileTemplate, GameObject bottomTemplate, int rotation = 0)
    {
        var tileInstance = (GameObject)Object.Instantiate(
             tileTemplate,
             tilePos.ToV3(),
             Quaternion.AngleAxis(rotation * 90, Vector3.up));
        tileInstance.transform.SetParent(_tilesRoot.transform);
        var bottomInstance = Object.Instantiate(bottomTemplate);
        bottomInstance.transform.SetParent(tileInstance.transform, false);

        return tileInstance;
    }

    public static void RemoveTile(TilePos tilePos)
    {
        if (HasAnyTileAt(tilePos))
        {
            GameObject.Destroy(Tiles[tilePos].Tile);
            Tiles.Remove(tilePos);
        }
    }

    public static Dictionary<TilePos, TileInfo> GetAllTiles()
    {
        return Tiles;
    }

    public static void SetAllTiles(Dictionary<TilePos, TileInfo> tiles)
    {
        _pauseEvents = true;
        ClearTiles();
        foreach (var tile in tiles)
        {
            AddOrReplaceTile(tile.Key, tile.Value.TileType, tile.Value.Rotation);
        }
        _pauseEvents = false;
    }

    public static void ClearTiles()
    {
        var tilePositions = Tiles.Select(x => x.Key).ToList();
        foreach (var tilePos in tilePositions)
        {
            RemoveTile(tilePos);
        }
    }
}