using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public Assets.CameraComponent camera { get { return (Assets.CameraComponent)GetComponent(ComponentIds.Camera); } }

        public bool hasCamera { get { return HasComponent(ComponentIds.Camera); } }

        static readonly Stack<Assets.CameraComponent> _cameraComponentPool = new Stack<Assets.CameraComponent>();

        public static void ClearCameraComponentPool() {
            _cameraComponentPool.Clear();
        }

        public Entity AddCamera(UnityEngine.Camera newValue) {
            var component = _cameraComponentPool.Count > 0 ? _cameraComponentPool.Pop() : new Assets.CameraComponent();
            component.Value = newValue;
            return AddComponent(ComponentIds.Camera, component);
        }

        public Entity ReplaceCamera(UnityEngine.Camera newValue) {
            var previousComponent = hasCamera ? camera : null;
            var component = _cameraComponentPool.Count > 0 ? _cameraComponentPool.Pop() : new Assets.CameraComponent();
            component.Value = newValue;
            ReplaceComponent(ComponentIds.Camera, component);
            if (previousComponent != null) {
                _cameraComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveCamera() {
            var component = camera;
            RemoveComponent(ComponentIds.Camera);
            _cameraComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherCamera;

        public static IMatcher Camera {
            get {
                if (_matcherCamera == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Camera);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherCamera = matcher;
                }

                return _matcherCamera;
            }
        }
    }
}
