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


public interface IRoomObject
{
    void Add(TilePos position, RoomInfoTwo roomInfo);
}

public class Tile : IRoomObject
{
    public readonly MainTileType Type;
    public TilePos Position { get; set; }
    public int? Rotation { get; set; }
    public string Subtype { get; set; }

    public Tile(MainTileType type)
    {
        Type = type;
    }

    public Tile Copy()
    {
        return new Tile(Type) { Position = Position, Rotation = Rotation };
    }

    public void Add(TilePos position, RoomInfoTwo roomInfo)
    {
        Position = position;
        roomInfo.AddOrReplaceTiles(this);
    }

    public bool CanHoldItem(TilePos position)
    {
        return true;
    }

#region Equality-members
    protected bool Equals(Tile other)
    {
        return Type == other.Type && Position.Equals(other.Position) && Rotation == other.Rotation && string.Equals(Subtype, other.Subtype);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }
        if (ReferenceEquals(this, obj))
        {
            return true;
        }
        if (obj.GetType() != this.GetType())
        {
            return false;
        }
        return Equals((Tile) obj);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = (int) Type;
            hashCode = (hashCode*397) ^ Position.GetHashCode();
            hashCode = (hashCode*397) ^ Rotation.GetHashCode();
            hashCode = (hashCode*397) ^ (Subtype != null ? Subtype.GetHashCode() : 0);
            return hashCode;
        }
    }
    #endregion
}

public class RoomInfoTwo
{
    private static RoomInfoTwo _instance;
    public static RoomInfoTwo Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new RoomInfoTwo();
            }
            return _instance;
        }
        set { _instance = value; }
    }

    private readonly Dictionary<TilePos, Tile> _tiles = new Dictionary<TilePos, Tile>();

    public void AddOrReplaceTiles(params Tile[] tiles)
    {
        var tileList = tiles.ToList();
        RemoveTiles(tiles.Select(x => x.Position).ToArray());

        foreach (var tile in tileList)
        {
            _tiles.Add(tile.Position, tile);
        }

        Events.instance.Raise(new TilesAddedTwo(tileList));
    }

    public bool HasAnyTileAt(TilePos pos)
    {
        return _tiles.ContainsKey(pos);
    }

    public bool HasTileAt(TilePos position, MainTileType type)
    {
        return HasAnyTileAt(position) && _tiles[position].Type == type;
    }

    public bool CanMoveTo(TilePos position)
    {
        return HasAnyTileAt(position) && _tiles[position].CanHoldItem(position);
    }

    public void RemoveTiles(params TilePos[] tilePositions)
    {
        var removedTilePositions = new List<TilePos>();
        foreach (var tilePosition in tilePositions.Where(HasAnyTileAt))
        {
            _tiles.Remove(tilePosition);
            removedTilePositions.Add(tilePosition);
        }

        Events.instance.Raise(new TilesRemoved(removedTilePositions));
    }

    public void ClearTiles()
    {
        RemoveTiles(_tiles.Select(x => x.Key).ToArray());
    }

    public List<Tile> GetAllTiles()
    {
        return _tiles.Select(x => x.Value).ToList();
    }

    public void SetAllTiles(Dictionary<TilePos, Tile> tiles)
    {
        ClearTiles();
        AddOrReplaceTiles(tiles.Select(x => x.Value).ToArray());
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

        var tileTemplate = TemplateLoader.Retrieve(type);

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