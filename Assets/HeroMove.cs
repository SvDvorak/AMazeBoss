using UnityEngine;

public class HeroMove : MonoBehaviour
{
	void Update ()
	{
        var currentPos = new TilePos(transform.position);
	    var moveDirection = new TilePos(0, 0);

        if (Input.GetKeyDown(KeyCode.UpArrow))
	    {
	        moveDirection = new TilePos(0, 1);
	    }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
	        moveDirection = new TilePos(0, -1);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            moveDirection = new TilePos(-1, 0);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            moveDirection = new TilePos(1, 0);
        }

	    if (RoomInfo.CanMoveTo(moveDirection + currentPos))
	    {
	        transform.Translate(moveDirection.ToV3());
	    }
	}
}