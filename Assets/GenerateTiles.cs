using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GenerateTiles : MonoBehaviour
{
    public int Columns;
    public int Rows;

    public GameObject Tile;
    public GameObject TileBottom;
    public float SideLength;

    [ContextMenu("Generate")]
    public void Generate()
    {
        var tilesParent = GetTilesParent();

        for (var z = 0; z < Rows; z++)
        {
            for (var x = 0; x < Columns; x++)
            {
                var tile = (GameObject)Instantiate(Tile, new Vector3(x*SideLength, 0, z*SideLength), Quaternion.AngleAxis(Random.Range(0, 4)*90, Vector3.up));
                tile.transform.SetParent(tilesParent.transform, true);

                var tileBottom = Instantiate(TileBottom);
                tileBottom.transform.SetParent(tile.transform, false);
            }
        }
    }

    [ContextMenu("ClearTiles")]
    public void Clear()
    {
        var tilesParent = GetTilesParent();

        var children = (from Transform child in tilesParent.transform select child.gameObject).ToList();
        children.ForEach(DestroyImmediate);
    }

    private static GameObject GetTilesParent()
    {
        var tilesParent = GameObject.Find("Tiles");

        if (tilesParent == null)
        {
            tilesParent = new GameObject("Tiles");
        }
        return tilesParent;
    }
}
