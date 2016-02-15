using Assets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneAfterDelay : MonoBehaviour
{
    public int Delay;
    public Scene Scene;

	public void Start ()
    {
        Invoke("LoadScene", Delay);
	}

    public void LoadScene()
    {
        SceneSetup.LoadScene("Play");
    }
}
