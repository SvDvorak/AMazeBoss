using UnityEngine;

public class CloseDialog : MonoBehaviour
{
    public void Close()
    {
        gameObject.SetActive(false);
    }
}