using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour
{
	public void Update ()
	{
	    if (Input.GetKeyDown(KeyCode.Escape) && LoadLevel.EditorLevelPath != "")
	    {
            RoomInfoTwo.Instance.ClearTiles();
            SceneManager.LoadScene("editor");
        }
    }
}
