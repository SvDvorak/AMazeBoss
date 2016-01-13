using System.Collections.Generic;
using DG.Tweening;
using Entitas;
using UnityEngine;

namespace Assets.Camera
{
    public class MoveAndRotateCameraSystem : IReactiveSystem, IEnsureComponents
    {
        public TriggerOnEvent trigger { get { return Matcher.AnyOf(Matcher.Rotation, Matcher.FocusPoint).OnEntityAdded(); } }
        public IMatcher ensureComponents { get { return Matcher.AllOf(Matcher.View, Matcher.Camera); } }

        public void Execute(List<Entity> entities)
        {
            var camera = entities.SingleEntity();
            var cameraTransform = camera.view.Value.transform;

            var currentRotation = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up);
            var nextRotation = camera.hasRotation ? camera.rotation.Value : 0;
            var focusPoint = camera.hasFocusPoint ? camera.focusPoint.DeltaPosition : Vector3.zero;

            DOTween
                .To(() => currentRotation, x => currentRotation = x, new Vector3(0, (45 + 90 * nextRotation) % 360, 0), 1)
                .OnUpdate(() => UpdateTransform(cameraTransform, focusPoint, currentRotation));
        }

        private void UpdateTransform(Transform transform, Vector3 focusPoint, Quaternion spin)
        {
            transform.position = focusPoint + spin * Vector3.back * 10 + new Vector3(0, 15, 0);
            transform.LookAt(focusPoint);
        }
    }
}
