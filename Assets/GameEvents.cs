using Assets;
using UnityEngine;

public class TileAdded : GameEvent
{
    public TilePos TilePos { get; private set; }
    public GameObject GameObject { get; private set; }

    public TileAdded(TilePos tilePos, GameObject gameObject)
    {
        TilePos = tilePos;
        GameObject = gameObject;
    }
}