using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.SavedFocusPointComponent savedFocusPoint { get { return (Assets.SavedFocusPointComponent)GetComponent(GameComponentIds.SavedFocusPoint); } }

        public bool hasSavedFocusPoint { get { return HasComponent(GameComponentIds.SavedFocusPoint); } }

        public Entity AddSavedFocusPoint(UnityEngine.Vector3 newPosition) {
            var componentPool = GetComponentPool(GameComponentIds.SavedFocusPoint);
            var component = (Assets.SavedFocusPointComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.SavedFocusPointComponent());
            component.Position = newPosition;
            return AddComponent(GameComponentIds.SavedFocusPoint, component);
        }

        public Entity ReplaceSavedFocusPoint(UnityEngine.Vector3 newPosition) {
            var componentPool = GetComponentPool(GameComponentIds.SavedFocusPoint);
            var component = (Assets.SavedFocusPointComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.SavedFocusPointComponent());
            component.Position = newPosition;
            ReplaceComponent(GameComponentIds.SavedFocusPoint, component);
            return this;
        }

        public Entity RemoveSavedFocusPoint() {
            return RemoveComponent(GameComponentIds.SavedFocusPoint);;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherSavedFocusPoint;

        public static IMatcher SavedFocusPoint {
            get {
                if (_matcherSavedFocusPoint == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.SavedFocusPoint);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherSavedFocusPoint = matcher;
                }

                return _matcherSavedFocusPoint;
            }
        }
    }
