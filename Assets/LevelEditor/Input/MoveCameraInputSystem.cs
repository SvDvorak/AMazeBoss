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
            _cameraGroup = _pool.GetGroup(GameMatcher.Camera);
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
                cameraEntity.ReplaceTargetFocusPoint(cameraEntity.targetFocusPoint.Position + deltaOffset);
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
}