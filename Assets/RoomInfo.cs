using System.Collections.Generic;
using Assets;
using UnityEngine;

public class RoomInfo
{
    private static readonly GameObject _tiles = new GameObject("Tiles");
    private static readonly Dictionary<TilePos, GameObject> _walkableTiles = new Dictionary<TilePos,GameObject>();

    public static bool CanWalk(TilePos pos)
    {
        return _walkableTiles.ContainsKey(pos);
    }

    public static void AddOrReplaceTile(TilePos tilePos, GameObject tileTemplate)
    {
        RemoveTile(tilePos);

        var tile = (GameObject)Object.Instantiate(tileTemplate, tilePos.ToV3(), Quaternion.AngleAxis(Random.Range(0, 4) * 90, Vector3.up));
        tile.transform.SetParent(_tiles.transform);
        _walkableTiles.Add(tilePos, tile);

        Events.instance.Raise(new TileAdded(tilePos, tile));
    }

    public static void RemoveTile(TilePos tilePos)
    {
        if (CanWalk(tilePos))
        {
            GameObject.Destroy(_walkableTiles[tilePos]);
            _walkableTiles.Remove(tilePos);
        }
    }
}