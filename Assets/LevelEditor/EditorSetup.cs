using UnityEngine;

namespace Assets.LevelEditor
{
    public class EditorSetup : MonoBehaviour
    {
        public static bool IsInEditor;

        public void Start ()
        {
            IsInEditor = true;
            RoomInfo.Init();

            var lastUsedPath = FileOperations.FileOperations.GetLastUsedPath();

            if (!string.IsNullOrEmpty(lastUsedPath))
            {
                FileOperations.FileOperations.Load(lastUsedPath);
            }
        }

        public void OnDestroy()
        {
            IsInEditor = false;
        }
    }
}
