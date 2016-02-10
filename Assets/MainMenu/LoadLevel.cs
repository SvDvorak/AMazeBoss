using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public void LoadPlay()
    {
        SceneManager.LoadScene("Play");
    }

    public void LoadEditor()
    {
        SceneManager.LoadScene("Editor");
    }
}