using Entitas;
using UnityEngine;

namespace Assets.LevelEditor.Input
{
    public class MouseInputSystem : IExecuteSystem, ISetPool
    {
        private Pool _pool;
        private Group _cameraGroup;

        public void SetPool(Pool pool)
        {
            _pool = pool;
            _cameraGroup = _pool.GetGroup(GameMatcher.Camera);
        }

        public void Execute()
        {
            var cameraEntity = _cameraGroup.GetSingleEntity();
            if (_pool.isPaused || cameraEntity == null)
            {
                return;
            }

            var currentPosition = GetMouseTilePosition(cameraEntity.camera.Value, UnityEngine.Input.mousePosition);
            var input = _pool.inputEntity;
            var hasMoved = !input.hasPosition || input.position.Value != currentPosition;

            if (hasMoved)
            {
                input.ReplacePosition(currentPosition);
            }

            input.isInputPlace = UnityEngine.Input.GetMouseButton(0);
            input.isInputRemove = UnityEngine.Input.GetMouseButton(1);
        }

        public static TilePos GetMouseTilePosition(UnityEngine.Camera camera, Vector2 mousePosition)
        {
            var ray = camera.ScreenPointToRay(mousePosition);
            var hPlane = new Plane(Vector3.up, Vector3.zero);
            float distance;
            if (hPlane.Raycast(ray, out distance))
            {
                var planePos = ray.GetPoint(distance);
                return new TilePos(planePos);
            }

            return new TilePos(0, 0);
        }
    }
}
