using Assets.FileOperations;
using Assets.LevelEditor;
using UnityEngine;

public class LoadLevelController : MonoBehaviour
{
    private LevelListFiller _levelListFiller;

    public GameObject InteractionsObject;

    public void OnEnable()
    {
        var interactions = InteractionsObject.GetComponent<UIInteractions>();
        var levelsInfo = PlayerPrefsLevelReader.GetLevelsInfo();
        _levelListFiller = GetComponentInChildren<LevelListFiller>();
        _levelListFiller.ShowLevels(levelsInfo, interactions);
    }

    public void OnDisable()
    {
        _levelListFiller.Clear();
    }
}