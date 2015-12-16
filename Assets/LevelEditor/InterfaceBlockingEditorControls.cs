using UnityEngine;

public class InterfaceBlockingEditorControls : MonoBehaviour
{
    public GameObject EditorInteractionsToDisable;

    public void MouseEnteredInterface()
    {
        if(EditorInteractionsToDisable != null)
        {
            EditorInteractionsToDisable.SetActive(false);
        }
    }

    public void MouseExitedInterface()
    {
        if(EditorInteractionsToDisable != null)
        {
            EditorInteractionsToDisable.SetActive(true);
        }
    }
}
