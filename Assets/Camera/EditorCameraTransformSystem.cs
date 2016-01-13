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
            var cameraTransform = entities.SingleEntity().view.Value.transform;

            var currentRotation = cameraTransform.rotation.eulerAngles.y;

            DOTween.To(angle => UpdateRotation(cameraTransform, angle), currentRotation, 90*entities.SingleEntity().rotation.Value, 5);
            //var transform = camera.view.Value.transform;
            //var clampedRotation = camera.rotation.Value%4;
            //Quaternion.AngleAxis(clampedRotation, Vector3.up);
            //transform.position = _defaultPosition + camera.CameraOffsetComponent.Position;
            //transform.DORotate(new Vector3(45, -45 + 90 * clampedRotation, 0), 3);
        }

        private void UpdateRotation(Transform transform, float angleInDegrees)
        {
            var angleInRadians = Mathf.Deg2Rad*angleInDegrees;
            transform.position = new Vector3(Mathf.Cos(angleInRadians)*10, transform.position.y, Mathf.Sin(angleInRadians)*10);
            transform.LookAt(Vector3.zero);
        }
    }
}
