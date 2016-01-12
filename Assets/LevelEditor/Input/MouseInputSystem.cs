using System.Collections.Generic;
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
            _cameraGroup = _pool.GetGroup(Matcher.Camera);
        }

        public void Execute()
        {
            var cameraEntity = _cameraGroup.GetSingleEntity();
            if (_pool.isPaused || cameraEntity == null)
            {
                return;
            }

            var currentPosition = GetMouseTilePosition(cameraEntity.camera.Value);
            var hasMoved = true;
            if (_pool.inputEntity.hasPosition)
            {
                hasMoved = _pool.inputEntity.position.Value != currentPosition;
            }

            if (hasMoved)
            {
                _pool.inputEntity.ReplacePosition(currentPosition);
            }

            var inputMoveDirection = GetInputMove();
            var rotation = GetInputRotation();

            var deltaOffset = inputMoveDirection*Time.deltaTime * ScrollMultiplier;
            if (deltaOffset != Vector3.zero)
            {
                cameraEntity.ReplaceCameraOffset(cameraEntity.cameraOffset.Position + deltaOffset);
            }
            if (rotation != 0)
            {
                cameraEntity.ReplaceRotation(cameraEntity.rotation.Value + rotation);
            }
        }

        private const float ScrollMultiplier = 6;

        private TilePos GetMouseTilePosition(UnityEngine.Camera camera)
        {
            var ray = camera.ScreenPointToRay(UnityEngine.Input.mousePosition);
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

        private readonly Dictionary<KeyCode, Vector3> _moveDirections = new Dictionary<KeyCode, Vector3>
            {
                { KeyCode.UpArrow, new Vector3(0, 0, 1) },
                { KeyCode.DownArrow, new Vector3(0, 0, -1) },
                { KeyCode.LeftArrow, new Vector3(-1, 0) },
                { KeyCode.RightArrow, new Vector3(1, 0) }
            };

        private Vector3 GetInputMove()
        {
            var inputMoveDirection = new Vector3();

            foreach (var moveDirection in _moveDirections)
            {
                if (UnityEngine.Input.GetKey(moveDirection.Key))
                {
                    inputMoveDirection = moveDirection.Value;
                }
            }
            return inputMoveDirection;
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
