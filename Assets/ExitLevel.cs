using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets
{
    public class ExitLevel : MonoBehaviour
    {
        public void Update ()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Escape) && LoadLevel.EditorLevelPath != "")
            {
                SceneManager.LoadScene("Editor");
            }
        }
    }
}
