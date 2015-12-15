using System.Collections.Generic;
using UnityEngine;

public class RoomInfo
{
    private static readonly Dictionary<TilePos, GameObject> _walkableTiles = new Dictionary<TilePos,GameObject>();

    public static bool CanWalk(TilePos pos)
    {
        return _walkableTiles.ContainsKey(pos);
    }

    public static void AddOrReplaceTile(TilePos tilePos, GameObject gameObject)
    {
        RemoveTile(tilePos);

        _walkableTiles.Add(tilePos, gameObject);
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