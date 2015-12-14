using UnityEngine;

public class HeroMove : MonoBehaviour
{
	void Update ()
	{
	    var multiplier = 2;
	    if (Input.GetKeyDown(KeyCode.UpArrow))
	    {
	        transform.Translate(Vector3.forward*multiplier);
	    }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.back * multiplier);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * multiplier);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * multiplier);
        }
	}
}
