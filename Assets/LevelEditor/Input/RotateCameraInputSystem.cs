using Entitas;
using UnityEngine;

namespace Assets.LevelEditor.Input
{
    public class RotateCameraInputSystem : IExecuteSystem, ISetPool
    {
        private Group _cameraGroup;
        private Pool _pool;

        public void SetPool(Pool pool)
        {
            _pool = pool;
            _cameraGroup = pool.GetGroup(Matcher.Camera);
        }

        public void Execute()
        {
            var cameraEntity = _cameraGroup.GetSingleEntity();
            if (_pool.isPaused || cameraEntity == null)
            {
                return;
            }

            var rotation = GetInputRotation();

            if (rotation != 0)
            {
                cameraEntity.ReplaceRotation(cameraEntity.rotation.Value + rotation);
            }
        }

        private static int GetInputRotation()
        {
            var rotation = 0;
            if (UnityEngine.Input.GetKeyDown(KeyCode.Q))
            {
                rotation = -1;
            }
            else if (UnityEngine.Input.GetKeyDown(KeyCode.E))
            {
                rotation = 1;
            }
            return rotation;
        }
    }
}