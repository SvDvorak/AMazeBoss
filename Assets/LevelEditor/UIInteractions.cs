using Entitas;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.LevelEditor
{
    public class UIInteractions : MonoBehaviour
    {
        public Text PositionInfo;

        private string _lastUsedPath = "";
        private bool _hasSetPath;

        public void Start()
        {
            _lastUsedPath = FileOperations.FileOperations.GetLastUsedPath();
            if (_lastUsedPath != "")
            {
                _hasSetPath = true;
            }
        }

        public void Save()
        {
            if (_hasSetPath)
            {
                SaveAs(_lastUsedPath);
            }
        }

        public void SaveAs(string path)
        {
            _lastUsedPath = path;
            _hasSetPath = true;
            FileOperations.FileOperations.Save(path);
        }

        public void Load(string path)
        {
            Clear();
            _lastUsedPath = path;
            _hasSetPath = true;
            FileOperations.FileOperations.Load(path);
        }

        public void Clear()
        {
            _hasSetPath = false;
            Pools.pool.Clear(Matcher.AnyOf(Matcher.Tile, Matcher.Item));
            EditorSetup.Instance.Update();
        }

        public void Play()
        {
            if(_hasSetPath)
            {
                SaveAs(_lastUsedPath);
                PlaySetup.FromEditor = true;
                SceneManager.LoadScene("Play");
            }
            else
            {
                Debug.Log("Can only play level if it has been saved to a file");
            }
        }

        public void Update()
        {
            if(Pools.pool.inputEntity.hasPosition)
            {
                var position = Pools.pool.inputEntity.position.Value;
                PositionInfo.text = string.Format("X: {0}\nZ: {1}", position.X, position.Z);
            }

            if (!Pools.pool.isPaused && UnityEngine.Input.GetKeyDown(KeyCode.Return))
            {
                Play();
            }
        }
    }
}