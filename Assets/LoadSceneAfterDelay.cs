using Assets;
using Assets.Level;
using UnityEngine;

public class LoadSceneAfterDelay : MonoBehaviour
{
    public int Delay;
    public UnityEngine.SceneManagement.Scene Scene;

	public void Start ()
    {
        Invoke("LoadScene", Delay);
	}

    public void LoadScene()
    {
        SceneSetup.LoadScene("MainMenu");
    }
}
