using Assets;
using UnityEngine;

public class TileAdded : GameEvent
{
    public TilePos TilePos { get; private set; }
    public MainTileType TileType { get; private set; }
    public GameObject GameObject { get; private set; }

    public TileAdded(TilePos tilePos, MainTileType tileType, GameObject gameObject)
    {
        TilePos = tilePos;
        TileType = tileType;
        GameObject = gameObject;
    }
}