using System.Collections.Generic;
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
    private static readonly Dictionary<TilePos, TileInfo> Tiles = new Dictionary<TilePos, TileInfo>();

    public static Dictionary<TilePos, TileInfo> GetAllTiles()
    {
        return Tiles;
    }

    public static bool HasTileAt(TilePos pos)
    {
        return Tiles.ContainsKey(pos);
    }

    public static void AddOrReplaceTile(TilePos tilePos, TileType tileType, GameObject tileTemplate)
    {
        RemoveTile(tilePos);

        var tile = (GameObject)Object.Instantiate(tileTemplate, tilePos.ToV3(), Quaternion.AngleAxis(Random.Range(0, 4) * 90, Vector3.up));
        tile.transform.SetParent(TilesRoot.transform);
        Tiles.Add(tilePos, new TileInfo(tileType, tile));

        Events.instance.Raise(new TileAdded(tilePos, tile));
    }

    public static void RemoveTile(TilePos tilePos)
    {
        if (HasTileAt(tilePos))
        {
            GameObject.Destroy(Tiles[tilePos].GameObject);
            Tiles.Remove(tilePos);
        }
    }
}