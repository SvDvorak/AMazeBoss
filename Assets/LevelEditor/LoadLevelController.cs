using Assets.FileOperations;
using Assets.LevelEditor;
using UnityEngine;

public class LoadLevelController : MonoBehaviour
{
    public GameObject ItemTemplate;
    public GameObject InteractionsObject;
    public GameObject ListRoot;

    public void OnEnable()
    {
        var levelsInfo = PlayerPrefsLevelReader.GetLevelsInfo();

        foreach (var level in levelsInfo.Levels)
        {
            var listItem = Instantiate(ItemTemplate);
            listItem.transform.SetParent(ListRoot.transform);
            listItem.GetComponent<LevelInfoController>().SetData(level, InteractionsObject);
        }
    }

    public void OnDisable()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}