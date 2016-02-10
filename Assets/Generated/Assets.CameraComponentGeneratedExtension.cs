using System.Collections.Generic;

using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.CameraComponent camera { get { return (Assets.CameraComponent)GetComponent(GameComponentIds.Camera); } }

        public bool hasCamera { get { return HasComponent(GameComponentIds.Camera); } }

        static readonly Stack<Assets.CameraComponent> _cameraComponentPool = new Stack<Assets.CameraComponent>();

        public static void ClearCameraComponentPool() {
            _cameraComponentPool.Clear();
        }

        public Entity AddCamera(UnityEngine.Camera newValue) {
            var component = _cameraComponentPool.Count > 0 ? _cameraComponentPool.Pop() : new Assets.CameraComponent();
            component.Value = newValue;
            return AddComponent(GameComponentIds.Camera, component);
        }

        public Entity ReplaceCamera(UnityEngine.Camera newValue) {
            var previousComponent = hasCamera ? camera : null;
            var component = _cameraComponentPool.Count > 0 ? _cameraComponentPool.Pop() : new Assets.CameraComponent();
            component.Value = newValue;
            ReplaceComponent(GameComponentIds.Camera, component);
            if (previousComponent != null) {
                _cameraComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveCamera() {
            var component = camera;
            RemoveComponent(GameComponentIds.Camera);
            _cameraComponentPool.Push(component);
            return this;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherCamera;

        public static IMatcher Camera {
            get {
                if (_matcherCamera == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.Camera);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherCamera = matcher;
                }

                return _matcherCamera;
            }
        }
    }
