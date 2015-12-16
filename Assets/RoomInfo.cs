using System.Collections.Generic;
using System.Linq;
using Assets;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

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

    public static bool HasTileAt(TilePos pos)
    {
        return Tiles.ContainsKey(pos);
    }

    public static void AddOrReplaceTile(TilePos tilePos, TileType tileType)
    {
        RemoveTile(tilePos);

        var tileTemplate = TileLoader.Retrieve(tileType);
        var tile = CreateTile(tilePos, tileTemplate);
        Tiles.Add(tilePos, new TileInfo(tileType, tile));

        Events.instance.Raise(new TileAdded(tilePos, tile));
    }

    private static GameObject CreateTile(TilePos tilePos, GameObject tileTemplate)
    {
        var tile = (GameObject)Object.Instantiate(tileTemplate, tilePos.ToV3(), Quaternion.AngleAxis(Random.Range(0, 4) * 90, Vector3.up));
        tile.transform.SetParent(TilesRoot.transform);
        return tile;
    }

    public static void RemoveTile(TilePos tilePos)
    {
        if (HasTileAt(tilePos))
        {
            GameObject.Destroy(Tiles[tilePos].GameObject);
            Tiles.Remove(tilePos);
        }
    }

    public static Dictionary<TilePos, TileInfo> GetAllTiles()
    {
        return Tiles;
    }

    public static void SetAllTiles(Dictionary<TilePos, TileType> tiles)
    {
        ClearTiles();
        foreach (var tile in tiles)
        {
            AddOrReplaceTile(tile.Key, tile.Value);
        }
    }

    private static void ClearTiles()
    {
        var tilePositions = Tiles.Select(x => x.Key).ToList();
        foreach (var tilePos in tilePositions)
        {
            RemoveTile(tilePos);
        }
    }
}