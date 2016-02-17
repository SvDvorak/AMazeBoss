using UnityEngine;

public class ShowIfDeveloper : MonoBehaviour
{
    public void Start()
    {
        if (!Debug.isDebugBuild)
        {
            gameObject.SetActive(false);
        }
    }
}