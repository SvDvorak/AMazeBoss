using UnityEngine;

namespace Assets.LevelEditor
{
    public class EditorSetup : MonoBehaviour
    {
        public void Start ()
        {
            RoomInfo.Init();

            if (LoadLevel.EditorLevelPath != "")
            {
                FileOperations.FileOperations.Load(LoadLevel.EditorLevelPath);
            }
        }
    }
}
