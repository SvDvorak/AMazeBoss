using UnityEngine;
using UnityEngine.UI;

public class LevelInfoSetter : MonoBehaviour
{
    public void SetData(string info)
    {
        GetComponentInChildren<Text>().text = info;
    }
}