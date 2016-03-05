using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.Camera.TargetFocusPointComponent targetFocusPoint { get { return (Assets.Camera.TargetFocusPointComponent)GetComponent(GameComponentIds.TargetFocusPoint); } }

        public bool hasTargetFocusPoint { get { return HasComponent(GameComponentIds.TargetFocusPoint); } }

        public Entity AddTargetFocusPoint(UnityEngine.Vector3 newPosition) {
            var componentPool = GetComponentPool(GameComponentIds.TargetFocusPoint);
            var component = (Assets.Camera.TargetFocusPointComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.Camera.TargetFocusPointComponent());
            component.Position = newPosition;
            return AddComponent(GameComponentIds.TargetFocusPoint, component);
        }

        public Entity ReplaceTargetFocusPoint(UnityEngine.Vector3 newPosition) {
            var componentPool = GetComponentPool(GameComponentIds.TargetFocusPoint);
            var component = (Assets.Camera.TargetFocusPointComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.Camera.TargetFocusPointComponent());
            component.Position = newPosition;
            ReplaceComponent(GameComponentIds.TargetFocusPoint, component);
            return this;
        }

        public Entity RemoveTargetFocusPoint() {
            return RemoveComponent(GameComponentIds.TargetFocusPoint);;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherTargetFocusPoint;

        public static IMatcher TargetFocusPoint {
            get {
                if (_matcherTargetFocusPoint == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.TargetFocusPoint);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherTargetFocusPoint = matcher;
                }

                return _matcherTargetFocusPoint;
            }
        }
    }
