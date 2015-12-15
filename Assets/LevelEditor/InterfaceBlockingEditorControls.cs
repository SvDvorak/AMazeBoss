using UnityEngine;

public class InterfaceBlockingEditorControls : MonoBehaviour
{
    public GameObject EditorInteractionsToDisable;

    public void MouseEnteredInterface()
    {
        EditorInteractionsToDisable.SetActive(false);
    }

    public void MouseExitedInterface()
    {
        EditorInteractionsToDisable.SetActive(true);
    }
}
