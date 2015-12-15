using UnityEngine;

public class RoomTilesInfo : MonoBehaviour
{
	void Start ()
	{
	    var tilesParent = GameObject.Find("Tiles");
	    foreach (Transform tile in tilesParent.transform)
	    {
	        if (tile.name.ToUpper().Contains("NORMAL"))
	        {
	            RoomInfo.AddOrReplaceTile(new TilePos(tile.transform.position/2), tile.gameObject);
	        }
	    }
	}

    void Update ()
	{
	
	}
}
