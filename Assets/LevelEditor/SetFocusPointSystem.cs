using Entitas;
using UnityEngine;

namespace Assets.LevelEditor
{
    public class SetFocusPointSystem : IExecuteSystem, ISetPool
    {
        private Group _cameraGroup;

        public void SetPool(Pool pool)
        {
            _cameraGroup = pool.GetGroup(Matcher.Camera);
        }

        public void Execute()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.F))
            {
                var camera = _cameraGroup.GetSingleEntity();
                camera.ReplaceSavedFocusPoint(camera.focusPoint.Position);
                Debug.Log("Set focus point at " + camera.focusPoint.Position);
            }
        }
    }
}
