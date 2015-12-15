using UnityEngine;

namespace Assets
{
    public class RoomTilesInfo : MonoBehaviour
    {
        void Start()
        {
            var tilesParent = GameObject.Find("Tiles");
            foreach (Transform tile in tilesParent.transform)
            {
                if (tile.gameObject.NameContains("normal"))
                {
                    RoomInfo.AddOrReplaceTile(new TilePos(tile.transform.position / 2), tile.gameObject);
                }
            }
        }
    }
}