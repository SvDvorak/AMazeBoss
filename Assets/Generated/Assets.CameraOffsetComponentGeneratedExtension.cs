using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public Assets.CameraOffsetComponent cameraOffset { get { return (Assets.CameraOffsetComponent)GetComponent(ComponentIds.CameraOffset); } }

        public bool hasCameraOffset { get { return HasComponent(ComponentIds.CameraOffset); } }

        static readonly Stack<Assets.CameraOffsetComponent> _cameraOffsetComponentPool = new Stack<Assets.CameraOffsetComponent>();

        public static void ClearCameraOffsetComponentPool() {
            _cameraOffsetComponentPool.Clear();
        }

        public Entity AddCameraOffset(UnityEngine.Vector3 newPosition) {
            var component = _cameraOffsetComponentPool.Count > 0 ? _cameraOffsetComponentPool.Pop() : new Assets.CameraOffsetComponent();
            component.Position = newPosition;
            return AddComponent(ComponentIds.CameraOffset, component);
        }

        public Entity ReplaceCameraOffset(UnityEngine.Vector3 newPosition) {
            var previousComponent = hasCameraOffset ? cameraOffset : null;
            var component = _cameraOffsetComponentPool.Count > 0 ? _cameraOffsetComponentPool.Pop() : new Assets.CameraOffsetComponent();
            component.Position = newPosition;
            ReplaceComponent(ComponentIds.CameraOffset, component);
            if (previousComponent != null) {
                _cameraOffsetComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveCameraOffset() {
            var component = cameraOffset;
            RemoveComponent(ComponentIds.CameraOffset);
            _cameraOffsetComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherCameraOffset;

        public static IMatcher CameraOffset {
            get {
                if (_matcherCameraOffset == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.CameraOffset);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherCameraOffset = matcher;
                }

                return _matcherCameraOffset;
            }
        }
    }
}
