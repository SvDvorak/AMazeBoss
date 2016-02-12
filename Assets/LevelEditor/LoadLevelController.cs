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
        var interactions = InteractionsObject.GetComponent<UIInteractions>();
        var levelsInfo = PlayerPrefsLevelReader.GetLevelsInfo();

        foreach (var level in levelsInfo.Levels)
        {
            var listItem = Instantiate(ItemTemplate);
            listItem.transform.SetParent(ListRoot.transform);
            listItem.GetComponent<LevelInfoController>().SetData(level, interactions);
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