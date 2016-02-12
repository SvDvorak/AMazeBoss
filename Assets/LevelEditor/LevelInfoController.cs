using Assets;
using Assets.FileOperations;
using Assets.LevelEditor;
using Entitas;
using UnityEngine;
using UnityEngine.UI;

public class LevelInfoController : MonoBehaviour
{
    private string _levelName;

    public void SetData(string levelName)
    {
        _levelName = levelName;
        GetComponentInChildren<Text>().text = _levelName;
    }

    public void Load()
    {
        PlayerPrefsLevelReader.LoadLevel(_levelName);
    }

    public void Delete()
    {
        PlayerPrefsLevelReader.Delete(_levelName);
        Destroy(gameObject);
    }
}