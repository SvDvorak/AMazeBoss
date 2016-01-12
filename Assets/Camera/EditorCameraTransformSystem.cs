using System.Collections.Generic;
using DG.Tweening;
using Entitas;
using UnityEngine;

namespace Assets.Camera
{
    public class EditorCameraTransformSystem : IReactiveSystem
    {
        private readonly Vector3 _defaultPosition = new Vector3(10, 15, -10);

        public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.Camera, Matcher.CameraOffset, Matcher.Rotation).OnEntityAdded(); } }

        public void Execute(List<Entity> entities)
        {
            var camera = entities.SingleEntity();

            var transform = camera.view.Value.transform;
            var clampedRotation = camera.rotation.Value%4;
            Quaternion.AngleAxis(clampedRotation, Vector3.up);
            transform.position = _defaultPosition + camera.cameraOffset.Position;
            transform.DORotate(new Vector3(45, -45 + 90 * clampedRotation, 0), 3);
        }
    }
}
