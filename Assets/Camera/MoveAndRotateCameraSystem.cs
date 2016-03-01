using System.Collections.Generic;
using DG.Tweening;
using Entitas;
using UnityEngine;

namespace Assets.Camera
{
    public class MoveAndRotateCameraSystem : IReactiveSystem, IEnsureComponents
    {
        private int _cameraRotationOffset = 35;

        public TriggerOnEvent trigger { get { return Matcher.AnyOf(GameMatcher.Rotation, GameMatcher.TargetFocusPoint).OnEntityAdded(); } }
        public IMatcher ensureComponents { get { return Matcher.AllOf(GameMatcher.View, GameMatcher.Camera); } }

        public void Execute(List<Entity> entities)
        {
            var camera = entities.SingleEntity();
            var cameraTransform = camera.view.Value.transform;

            var currentRotation = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up);

            if (!camera.hasCurrentFocusPoint)
            {
                camera.ReplaceCurrentFocusPoint(camera.targetFocusPoint.Position);
            }

            var currentFocus = camera.currentFocusPoint.Position;

            DOTween.Sequence()
                .Append(DOTween.To(() => camera.currentFocusPoint.Position, x => camera.ReplaceCurrentFocusPoint(x), camera.targetFocusPoint.Position, 1))
                .Join(DOTween.To(() => currentRotation, x => currentRotation = x, new Vector3(0, (_cameraRotationOffset + 90 * camera.rotation.Value) % 360, 0), 1))
                .OnUpdate(() => UpdateTransform(cameraTransform, camera.currentFocusPoint.Position, currentRotation));
        }

        private void UpdateTransform(Transform transform, Vector3 focusPoint, Quaternion spin)
        {
            transform.position = focusPoint + spin * Vector3.back * 10 + new Vector3(0, 15, 0);
            transform.LookAt(focusPoint);
        }
    }
}
