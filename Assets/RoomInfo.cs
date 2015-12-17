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


public class RoomInfo
{
    private static RoomInfo _instance;
    public static RoomInfo Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new RoomInfo();
            }
            return _instance;
        }
        set { _instance = value; }
    }

    private GameObject _tilesRoot;
    private readonly Dictionary<TilePos, TileInfo> Tiles = new Dictionary<TilePos, TileInfo>();
    private readonly HashSet<MainTileType> WalkableTiles = new HashSet<MainTileType>()
    {
        MainTileType.Normal,
        MainTileType.Spike,
        MainTileType.Hero,
        MainTileType.Boss
    };

    private bool _pauseEvents;

    public RoomInfo()
    {
        Events.instance.AddListener<LoadingScene>(x => ClearTiles());
    }

    public void Init()
    {
        _tilesRoot = new GameObject("Tiles");
    }

    public bool HasAnyTileAt(TilePos pos)
    {
        return Tiles.ContainsKey(pos);
    }

    public bool HasTileAt(TilePos pos, MainTileType type)
    {
        return Tiles.ContainsKey(pos) && Tiles[pos].TileType.Main == type;
    }

    public bool CanMoveTo(TilePos pos)
    {
        return HasAnyTileAt(pos) && WalkableTiles.Contains(Tiles[pos].TileType.Main);
    }

    public void AddOrReplaceTile(TilePos tilePos, MainTileType type)
    {
        AddOrReplaceTile(tilePos, new CompleteTileType(type));
    }

    public void AddOrReplaceTile(TilePos tilePos, CompleteTileType type, int? preferredRotation = null)
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

    private GameObject CreateParented(TilePos tilePos, GameObject tileTemplate, GameObject bottomTemplate, int rotation = 0)
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

    public void RemoveTile(TilePos tilePos)
    {
        if (HasAnyTileAt(tilePos))
        {
            GameObject.Destroy(Tiles[tilePos].Tile);
            Tiles.Remove(tilePos);
        }
    }

    public Dictionary<TilePos, TileInfo> GetAllTiles()
    {
        return Tiles;
    }

    public void SetAllTiles(Dictionary<TilePos, TileInfo> tiles)
    {
        _pauseEvents = true;
        ClearTiles();
        foreach (var tile in tiles)
        {
            AddOrReplaceTile(tile.Key, tile.Value.TileType, tile.Value.Rotation);
        }
        _pauseEvents = false;
    }

    public void ClearTiles()
    {
        var tilePositions = Tiles.Select(x => x.Key).ToList();
        foreach (var tilePos in tilePositions)
        {
            RemoveTile(tilePos);
        }
    }
}