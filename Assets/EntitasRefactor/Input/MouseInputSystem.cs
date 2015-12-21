using Entitas;
using UnityEngine;

namespace Assets.EntitasRefactor.Input
{
    public class MouseInputSystem : IExecuteSystem, ISetPool
    {
        private Pool _pool;

        public void SetPool(Pool pool)
        {
            _pool = pool;
        }

        public void Execute()
        {
            var currentPosition = GetMouseTilePosition();
            var hasMoved = true;
            if (_pool.inputEntity.hasPosition)
            {
                hasMoved = _pool.inputEntity.position.Value != currentPosition;
            }

            if (hasMoved)
            {
                _pool.inputEntity.ReplacePosition(currentPosition);
            }
        }

        private static TilePos GetMouseTilePosition()
        {
            var ray = Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition);
            var hPlane = new Plane(Vector3.up, Vector3.zero);
            float distance;
            if (hPlane.Raycast(ray, out distance))
            {
                var planePos = ray.GetPoint(distance);
                var tilePos = new TilePos(planePos);

                return tilePos;
            }

            return new TilePos(0, 0);
        }
    }
}
