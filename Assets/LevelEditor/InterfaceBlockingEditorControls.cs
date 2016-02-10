using UnityEngine;

namespace Assets.LevelEditor
{
    public class InterfaceBlockingEditorControls : MonoBehaviour
    {
        public void MouseExitedInterface()
        {
            Pools.game.isPaused = false;
        }

        public void MouseEnteredInterface()
        {
            Pools.game.isPaused = true;
        }
    }
}
