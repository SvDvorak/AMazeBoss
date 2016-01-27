using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Assets.LevelEditor.Input
{
    public class MoveCameraInputSystem : IExecuteSystem, ISetPool
    {
        private Pool _pool;
        private Group _cameraGroup;

        private const float ScrollMultiplier = 10;

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

            var inputMoveDirection = GetInputMove(cameraEntity);

            var deltaOffset = inputMoveDirection * Time.deltaTime * ScrollMultiplier;
            if (deltaOffset != Vector3.zero)
            {
                cameraEntity.ReplaceFocusPoint(cameraEntity.focusPoint.Position + deltaOffset);
            }
        }

        private readonly Dictionary<KeyCode, Vector3> _moveDirections = new Dictionary<KeyCode, Vector3>
            {
                { KeyCode.UpArrow, new Vector3(0, 0, 1) },
                { KeyCode.DownArrow, new Vector3(0, 0, -1) },
                { KeyCode.LeftArrow, new Vector3(-1, 0) },
                { KeyCode.RightArrow, new Vector3(1, 0) }
            };

        private Vector3 GetInputMove(Entity camera)
        {
            var inputMoveDirection = new Vector3();

            foreach (var moveDirection in _moveDirections)
            {
                if (UnityEngine.Input.GetKey(moveDirection.Key))
                {
                    inputMoveDirection = moveDirection.Value;
                }
            }

            if (_cameraGroup.count != 0)
            {
                inputMoveDirection = Quaternion.AngleAxis(camera.rotation.Value * 90, Vector3.up)*inputMoveDirection;
            }

            return inputMoveDirection;
        }
    }

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
        }

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
    }
}
