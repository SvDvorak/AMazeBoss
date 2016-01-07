using UnityEngine;

namespace Assets
{
    public class LoadLevel : MonoBehaviour
    {
        public static string EditorLevelPath = "";
        public string LevelPath;

        public void Start ()
        {
            if(EditorLevelPath != "")
            {
                LevelPath = EditorLevelPath;
            }

            FileOperations.FileOperations.Load(LevelPath);
        }
    }
}
