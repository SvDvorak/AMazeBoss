using Assets.FileOperations;
using UnityEngine;

public class LoadLevelController : MonoBehaviour
{
    private LevelListFiller _levelListFiller;

    public void OnEnable()
    {
        var levelsInfo = PlayerPrefsLevelReader.GetLevelsInfo();
        _levelListFiller = GetComponentInChildren<LevelListFiller>();
        _levelListFiller.ShowLevels(levelsInfo);
    }

    public void OnDisable()
    {
        _levelListFiller.Clear();
    }
}