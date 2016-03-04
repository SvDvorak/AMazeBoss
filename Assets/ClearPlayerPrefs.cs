using UnityEngine;

public class ClearPlayerPrefs : MonoBehaviour
{
    public void Start()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Cleared player prefs - levels are now removed");
    }
}