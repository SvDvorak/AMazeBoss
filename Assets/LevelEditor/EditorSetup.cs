using UnityEngine;

namespace Assets.LevelEditor
{
    public class EditorSetup : MonoBehaviour
    {
        public void Start ()
        {
            RoomInfo.Init();

            var lastUsedPath = FileOperations.FileOperations.GetLastUsedPath();

            if (lastUsedPath != "")
            {
                FileOperations.FileOperations.Load(lastUsedPath);
            }
        }
    }
}
