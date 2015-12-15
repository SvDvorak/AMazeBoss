using System.Collections.Generic;
using UnityEngine;

public class ClickPlane : MonoBehaviour
{
    public List<GameObject> Tiles;
	
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var mouseTilePosition = GetMouseTilePosition();
            if (mouseTilePosition != null)
            {
                AddOrReplaceTile(mouseTilePosition.TilePos);
            }
        }
        else if(Input.GetMouseButtonDown(1))
        {
            var mouseTilePosition = GetMouseTilePosition();
            if (mouseTilePosition != null)
            {
                RoomInfo.RemoveTile(mouseTilePosition.TilePos);
            }
        }
    }

    private class MouseTile
    {
        public MouseTile(TilePos tilePos)
        {
            TilePos = tilePos;
        }

        public TilePos TilePos { get; private set; }
    }

    private static MouseTile GetMouseTilePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane hPlane = new Plane(Vector3.up, Vector3.zero);
        float distance = 0;
        if (hPlane.Raycast(ray, out distance))
        {
            var planePos = ray.GetPoint(distance);
            var tilePos = new TilePos(planePos);

            return new MouseTile(tilePos);
        }

        return null;
    }

    private void AddOrReplaceTile(TilePos tilePos)
    {
        var tileTemplate = Tiles[Random.Range(0, Tiles.Count)];
        var tileInstance = (GameObject)Instantiate(tileTemplate, tilePos.ToV3(), Quaternion.AngleAxis(Random.Range(0, 4)*90, Vector3.up));
        RoomInfo.AddOrReplaceTile(tilePos, tileInstance);
    }
}
