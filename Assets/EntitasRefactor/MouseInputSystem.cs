using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;
using UnityEngine;

namespace Assets.EntitasRefactor
{
    public class MouseInputSystem : IExecuteSystem, ISetPool
    {
        private Group _inputGroup;

        public void SetPool(Pool pool)
        {
            _inputGroup = pool.GetGroup(Matcher.Input);
        }

        public void Execute()
        {
            var currentPosition = GetMouseTilePosition();
            var hasMoved = true;
            var mouseEntity = _inputGroup.GetSingleEntity();
            if (mouseEntity.hasPosition)
            {
                hasMoved = mouseEntity.position.Value != currentPosition;
            }

            if (hasMoved)
            {
                mouseEntity.ReplacePosition(currentPosition);
            }
        }

        private static TilePos GetMouseTilePosition()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
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
