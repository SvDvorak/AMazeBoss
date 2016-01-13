using System.Collections.Generic;
using DG.Tweening;
using Entitas;
using UnityEngine;

namespace Assets.Camera
{
    public class EditorCameraTransformSystem : IReactiveSystem, IEnsureComponents
    {
        public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.Camera, Matcher.CameraOffset, Matcher.Rotation).OnEntityAdded(); } }
        public IMatcher ensureComponents { get { return Matcher.View; } }

        public void Execute(List<Entity> entities)
        {
            var camera = entities.SingleEntity();
            var cameraTransform = camera.view.Value.transform;
            var currentRotation = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up);

            DOTween
                .To(() => currentRotation, x => currentRotation = x, new Vector3(0, 45 + 90 * camera.rotation.Value, 0), 1)
                .OnUpdate(() => UpdateRotation(cameraTransform, currentRotation));
        }

        private void UpdateRotation(Transform transform, Quaternion spin)
        {
            transform.position = spin*Vector3.back*10 + new Vector3(0, 15, 0);
            transform.LookAt(Vector3.zero);
        }
    }
}
