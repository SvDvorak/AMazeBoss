using Assets.FileOperations;
using UnityEngine;

public class LevelListFiller : MonoBehaviour
{
    public GameObject ItemTemplate;

    public void ShowLevels(LevelsInfo levelsInfo)
    {
        foreach (var level in levelsInfo.Levels)
        {
            var listItem = Instantiate(ItemTemplate);
            listItem.transform.SetParent(transform);
            listItem.GetComponent<LevelInfoSetter>().SetData(level);
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