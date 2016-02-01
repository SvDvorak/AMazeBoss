using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public Assets.SavedFocusPointComponent savedFocusPoint { get { return (Assets.SavedFocusPointComponent)GetComponent(ComponentIds.SavedFocusPoint); } }

        public bool hasSavedFocusPoint { get { return HasComponent(ComponentIds.SavedFocusPoint); } }

        static readonly Stack<Assets.SavedFocusPointComponent> _savedFocusPointComponentPool = new Stack<Assets.SavedFocusPointComponent>();

        public static void ClearSavedFocusPointComponentPool() {
            _savedFocusPointComponentPool.Clear();
        }

        public Entity AddSavedFocusPoint(UnityEngine.Vector3 newPosition) {
            var component = _savedFocusPointComponentPool.Count > 0 ? _savedFocusPointComponentPool.Pop() : new Assets.SavedFocusPointComponent();
            component.Position = newPosition;
            return AddComponent(ComponentIds.SavedFocusPoint, component);
        }

        public Entity ReplaceSavedFocusPoint(UnityEngine.Vector3 newPosition) {
            var previousComponent = hasSavedFocusPoint ? savedFocusPoint : null;
            var component = _savedFocusPointComponentPool.Count > 0 ? _savedFocusPointComponentPool.Pop() : new Assets.SavedFocusPointComponent();
            component.Position = newPosition;
            ReplaceComponent(ComponentIds.SavedFocusPoint, component);
            if (previousComponent != null) {
                _savedFocusPointComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveSavedFocusPoint() {
            var component = savedFocusPoint;
            RemoveComponent(ComponentIds.SavedFocusPoint);
            _savedFocusPointComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherSavedFocusPoint;

        public static IMatcher SavedFocusPoint {
            get {
                if (_matcherSavedFocusPoint == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.SavedFocusPoint);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherSavedFocusPoint = matcher;
                }

                return _matcherSavedFocusPoint;
            }
        }
    }
}
