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

        public void Start()
        {
            _lastUsedName = PlayerPrefsLevelReader.LastUsedLevelName;
        }

        public void Save()
        {
            if (!string.IsNullOrEmpty(_lastUsedName))
            {
                SaveAs(_lastUsedName);
            }
        }

        public void SaveAs(string levelName)
        {
            _lastUsedName = levelName;

            PlayerPrefsLevelReader.SaveLevel(levelName, LevelLoader.CreateLevelData(Pools.game));
        }

        public void Load(string levelName)
        {
            _lastUsedName = levelName;
            PlayerPrefsLevelReader.LoadLevel(levelName);
        }

        public void Delete(string levelName)
        {
            PlayerPrefsLevelReader.Delete(levelName);
        }

        public void Clear()
        {
            Pools.game.Clear(Matcher.AnyOf(GameMatcher.Tile, GameMatcher.Item));
            EditorSetup.Instance.Update();
        }

        public void Play()
        {
            PlaySetup.FromEditor = true;
            PlaySetup.EditorLevel = PlayerPrefsLevelReader.GetLevel(_lastUsedName);
            SceneManager.LoadScene("Play");
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