using System.Collections.Generic;
using System.Linq;
using Assets;
using UnityEngine;

public class TileInfo
{
    public TileType Type { get; private set; }
    public GameObject GameObject { get; private set; }

    public TileInfo(TileType type, GameObject gameObject)
    {
        Type = type;
        GameObject = gameObject;
    }
}

public class RoomInfo
{
    private static readonly GameObject TilesRoot = new GameObject("Tiles");
    private static Dictionary<TilePos, TileInfo> _tiles = new Dictionary<TilePos, TileInfo>();

    public static Dictionary<TilePos, TileInfo> GetAllTiles()
    {
        return _tiles;
    }

    public static bool HasTileAt(TilePos pos)
    {
        return _tiles.ContainsKey(pos);
    }

    public static void AddOrReplaceTile(TilePos tilePos, TileType tileType, GameObject tileTemplate)
    {
        RemoveTile(tilePos);

        var tile = (GameObject)Object.Instantiate(tileTemplate, tilePos.ToV3(), Quaternion.AngleAxis(Random.Range(0, 4) * 90, Vector3.up));
        tile.transform.SetParent(TilesRoot.transform);
        _tiles.Add(tilePos, new TileInfo(tileType, tile));

        Events.instance.Raise(new TileAdded(tilePos, tile));
    }

    public static void RemoveTile(TilePos tilePos)
    {
        if (HasTileAt(tilePos))
        {
            GameObject.Destroy(_tiles[tilePos].GameObject);
            _tiles.Remove(tilePos);
        }
    }

    public static void SetAllTiles(Dictionary<TilePos, TileType> tiles)
    {
        foreach (var tile in tiles)
        {
            _tiles.Add(tile.Key, new TileInfo(tile.Value, null));
        }
    }
}