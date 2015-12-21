using System.Collections.Generic;
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

public class TilesAddedTwo : GameEvent
{
    public List<StandardTile> Tiles { get; private set; }

    public TilesAddedTwo(List<StandardTile> tiles)
    {
        Tiles = tiles;
    }
}

public class TilesRemoved : GameEvent
{
    public List<TilePos> TilePositions { get; private set; }

    public TilesRemoved(List<TilePos> tilePositions)
    {
        TilePositions = tilePositions;
    }
}