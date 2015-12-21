using Assets.EntitasRefactor;
using Entitas;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.LevelEditor
{
    public class UIInteractions : MonoBehaviour
    {
        private string _lastUsedPath = "";

        public void Start()
        {
            _lastUsedPath = FileOperations.FileOperations.GetLastUsedPath();
        }

        public void Save()
        {
            if (_lastUsedPath != "")
            {
                SaveAs(_lastUsedPath);
            }
        }

        public void SaveAs(string path)
        {
            SetLastUsedPath(path);
            FileOperations.FileOperations.Save(path);
        }

        public void Load(string path)
        {
            Clear();
            SetLastUsedPath(path);
            FileOperations.FileOperations.Load(path);
        }

        public void Clear()
        {
            SetLastUsedPath("");
            Pools.pool.Clear(Matcher.Tile);
        }

        private void SetLastUsedPath(string path)
        {
            _lastUsedPath = path;
            LoadLevel.EditorLevelPath = path;
            PlayerPrefs.SetString("LastEditorLevel", path);
        }

        public void Play()
        {
            if(_lastUsedPath != "")
            {
                SaveAs(_lastUsedPath);
                Events.instance.Raise(new LoadingScene());
                SceneManager.LoadScene("PlayScene");
            }
        }
    }
}