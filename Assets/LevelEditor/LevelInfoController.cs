using Assets.LevelEditor;
using UnityEngine;
using UnityEngine.UI;

public class LevelInfoController : MonoBehaviour
{
    private string _levelName;
    private UIInteractions _interactions;

    public void SetData(string levelName, UIInteractions interactions)
    {
        _levelName = levelName;
        _interactions = interactions;
        GetComponentInChildren<Text>().text = _levelName;
    }

    public void Load()
    {
        _interactions.Load(_levelName);
    }

    public void Delete()
    {
        _interactions.Delete(_levelName);
        Destroy(gameObject);
    }
}