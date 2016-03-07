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

            Clear();
            PlayerPrefsLevelReader.LoadLevel(levelName);
        }

        public void Import(string filePath)
        {
            var level = FileOperations.FileOperations.Load(filePath);
            var fileNameStart = filePath.LastIndexOf("\\") + 1;
            var nameLength = filePath.Length - fileNameStart;
            var fileName = filePath.Substring(fileNameStart, nameLength);
            PlayerPrefsLevelReader.SaveLevel(fileName, level, false);
        }

        public void Export(string filePath)
        {
            FileOperations.FileOperations.Save(filePath);
        }

        public void Delete(string levelName)
        {
            PlayerPrefsLevelReader.Delete(levelName);
        }

        public void Clear()
        {
            Pools.game.SafeDeleteLevel();
            EditorSetup.Instance.Update();
        }

        public void Play()
        {
            PlaySetup.EditorLevel = LevelLoader.CreateLevelData(Pools.game);
            SceneSetup.LoadScene("Play");
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