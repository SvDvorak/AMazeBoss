using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.FocusPointComponent focusPoint { get { return (Assets.FocusPointComponent)GetComponent(GameComponentIds.FocusPoint); } }

        public bool hasFocusPoint { get { return HasComponent(GameComponentIds.FocusPoint); } }

        public Entity AddFocusPoint(UnityEngine.Vector3 newPosition) {
            var componentPool = GetComponentPool(GameComponentIds.FocusPoint);
            var component = (Assets.FocusPointComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.FocusPointComponent());
            component.Position = newPosition;
            return AddComponent(GameComponentIds.FocusPoint, component);
        }

        public Entity ReplaceFocusPoint(UnityEngine.Vector3 newPosition) {
            var componentPool = GetComponentPool(GameComponentIds.FocusPoint);
            var component = (Assets.FocusPointComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.FocusPointComponent());
            component.Position = newPosition;
            ReplaceComponent(GameComponentIds.FocusPoint, component);
            return this;
        }

        public Entity RemoveFocusPoint() {
            return RemoveComponent(GameComponentIds.FocusPoint);;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherFocusPoint;

        public static IMatcher FocusPoint {
            get {
                if (_matcherFocusPoint == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.FocusPoint);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherFocusPoint = matcher;
                }

                return _matcherFocusPoint;
            }
        }
    }
