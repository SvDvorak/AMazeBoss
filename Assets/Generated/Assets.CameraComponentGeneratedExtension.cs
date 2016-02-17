using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.CameraComponent camera { get { return (Assets.CameraComponent)GetComponent(GameComponentIds.Camera); } }

        public bool hasCamera { get { return HasComponent(GameComponentIds.Camera); } }

        public Entity AddCamera(UnityEngine.Camera newValue) {
            var componentPool = GetComponentPool(GameComponentIds.Camera);
            var component = (Assets.CameraComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.CameraComponent());
            component.Value = newValue;
            return AddComponent(GameComponentIds.Camera, component);
        }

        public Entity ReplaceCamera(UnityEngine.Camera newValue) {
            var componentPool = GetComponentPool(GameComponentIds.Camera);
            var component = (Assets.CameraComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.CameraComponent());
            component.Value = newValue;
            ReplaceComponent(GameComponentIds.Camera, component);
            return this;
        }

        public Entity RemoveCamera() {
            return RemoveComponent(GameComponentIds.Camera);;
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
