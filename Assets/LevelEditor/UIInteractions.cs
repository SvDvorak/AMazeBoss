using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.LevelEditor
{
    public class UIInteractions : MonoBehaviour
    {
        private string _lastUsedPath = "";

        public void Start()
        {
            if (LoadLevel.EditorLevelPath != "")
            {
                _lastUsedPath = LoadLevel.EditorLevelPath;
            }
        }

        public void Save(string path)
        {
            _lastUsedPath = path;
            FileOperations.FileOperations.Save(path);
        }

        public void Load(string path)
        {
            _lastUsedPath = path;
            FileOperations.FileOperations.Load(path);
        }

        public void Clear()
        {
            RoomInfo.ClearTiles();
        }

        public void Play()
        {
            if(_lastUsedPath != "")
            {
                Save(_lastUsedPath);
                RoomInfo.ClearTiles();
                LoadLevel.EditorLevelPath = _lastUsedPath;
                SceneManager.LoadScene("test_scene");
            }
        }
    }
}