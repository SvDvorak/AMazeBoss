using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.Camera.CurrentFocusPointComponent currentFocusPoint { get { return (Assets.Camera.CurrentFocusPointComponent)GetComponent(GameComponentIds.CurrentFocusPoint); } }

        public bool hasCurrentFocusPoint { get { return HasComponent(GameComponentIds.CurrentFocusPoint); } }

        public Entity AddCurrentFocusPoint(UnityEngine.Vector3 newPosition) {
            var componentPool = GetComponentPool(GameComponentIds.CurrentFocusPoint);
            var component = (Assets.Camera.CurrentFocusPointComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.Camera.CurrentFocusPointComponent());
            component.Position = newPosition;
            return AddComponent(GameComponentIds.CurrentFocusPoint, component);
        }

        public Entity ReplaceCurrentFocusPoint(UnityEngine.Vector3 newPosition) {
            var componentPool = GetComponentPool(GameComponentIds.CurrentFocusPoint);
            var component = (Assets.Camera.CurrentFocusPointComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.Camera.CurrentFocusPointComponent());
            component.Position = newPosition;
            ReplaceComponent(GameComponentIds.CurrentFocusPoint, component);
            return this;
        }

        public Entity RemoveCurrentFocusPoint() {
            return RemoveComponent(GameComponentIds.CurrentFocusPoint);;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherCurrentFocusPoint;

        public static IMatcher CurrentFocusPoint {
            get {
                if (_matcherCurrentFocusPoint == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.CurrentFocusPoint);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherCurrentFocusPoint = matcher;
                }

                return _matcherCurrentFocusPoint;
            }
        }
    }
