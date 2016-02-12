using Assets.FileOperations;
using Assets.LevelEditor;
using UnityEngine;

public class LevelListFiller : MonoBehaviour
{
    public GameObject ItemTemplate;

    public void ShowLevels(LevelsInfo levelsInfo, UIInteractions interactions)
    {
        foreach (var level in levelsInfo.Levels)
        {
            var listItem = Instantiate(ItemTemplate);
            listItem.transform.SetParent(transform);
            listItem.GetComponent<LevelInfoController>().SetData(level, interactions);
        }
    }

    public void Clear()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}