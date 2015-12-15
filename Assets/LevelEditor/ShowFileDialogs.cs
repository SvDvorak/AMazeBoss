using UnityEngine;

public class ShowFileDialogs : MonoBehaviour
{
    public GameObject SaveDialog;
    public GameObject LoadDialog;

    public void ShowSave()
    {
        LoadDialog.SetActive(false);
        SaveDialog.SetActive(true);
    }

    public void ShowLoad()
    {
        SaveDialog.SetActive(false);
        LoadDialog.SetActive(true);
    }
}
