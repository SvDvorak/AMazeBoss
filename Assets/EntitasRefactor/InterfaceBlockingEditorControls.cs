using UnityEngine;
using System.Collections;

public class InterfaceBlockingEditorControls : MonoBehaviour
{
    public void MouseExitedInterface()
    {
        Pools.pool.isPaused = false;
    }

    public void MouseEnteredInterface()
    {
        Pools.pool.isPaused = true;
    }
}
