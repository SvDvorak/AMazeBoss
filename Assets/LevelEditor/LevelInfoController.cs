using Assets.LevelEditor;
using UnityEngine;
using UnityEngine.UI;

public class LevelInfoController : MonoBehaviour
{
    private string _levelName;
    private UIInteractions _interactions;
    private ShowFileDialogs _dialogController;


    public void SetData(string levelName, GameObject interactionsObject)
    {
        _levelName = levelName;
        _interactions = interactionsObject.GetComponent<UIInteractions>();
        _dialogController = interactionsObject.GetComponent<ShowFileDialogs>();
        GetComponentInChildren<Text>().text = _levelName;
    }

    public void Load()
    {
        _interactions.Load(_levelName);
    }

    public void Export()
    {
        _dialogController.ShowExport();
        //_interactions.Export(_levelName);
    }

    public void Delete()
    {
        _interactions.Delete(_levelName);
        Destroy(gameObject);
    }
}