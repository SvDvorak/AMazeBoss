using System.Collections.Generic;
using System.Linq;
using Assets.FileOperations;
using Entitas;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.LevelEditor
{
    public class UIInteractions : MonoBehaviour
    {
        public Text PositionInfo;

        private string _lastUsedName = "";
        private bool _hasSetPath;

        public void Start()
        {
            _lastUsedName = FileOperations.FileOperations.GetLastUsedPath();
            if (_lastUsedName != "")
            {
                _hasSetPath = true;
            }
        }

        public void Save()
        {
            if (_hasSetPath)
            {
                SaveAs(_lastUsedName);
            }
        }

        public void SaveAs(string name)
        {
            _lastUsedName = name;
            _hasSetPath = true;

            PlayerPrefsLevelReader.SaveLevel(name, LevelLoader.CreateLevelData(Pools.game));
        }

        public void Load(string path)
        {
            //Clear();
            //_lastUsedName = path;
            //_hasSetPath = true;
            //FileOperations.FileOperations.Load(path);
        }

        public void Clear()
        {
            _hasSetPath = false;
            Pools.game.Clear(Matcher.AnyOf(GameMatcher.Tile, GameMatcher.Item));
            EditorSetup.Instance.Update();
        }

        public void Play()
        {
            if(_hasSetPath)
            {
                SaveAs(_lastUsedName);
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
            if(Pools.game.inputEntity.hasPosition)
            {
                var position = Pools.game.inputEntity.position.Value;
                PositionInfo.text = string.Format("X: {0}\nZ: {1}", position.X, position.Z);
            }

            if (!Pools.game.isPaused && UnityEngine.Input.GetKeyDown(KeyCode.Return))
            {
                Play();
            }
        }
    }
}