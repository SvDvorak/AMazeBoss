using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public Assets.FocusPointComponent focusPoint { get { return (Assets.FocusPointComponent)GetComponent(ComponentIds.FocusPoint); } }

        public bool hasFocusPoint { get { return HasComponent(ComponentIds.FocusPoint); } }

        static readonly Stack<Assets.FocusPointComponent> _focusPointComponentPool = new Stack<Assets.FocusPointComponent>();

        public static void ClearFocusPointComponentPool() {
            _focusPointComponentPool.Clear();
        }

        public Entity AddFocusPoint(UnityEngine.Vector3 newDeltaPosition) {
            var component = _focusPointComponentPool.Count > 0 ? _focusPointComponentPool.Pop() : new Assets.FocusPointComponent();
            component.Position = newDeltaPosition;
            return AddComponent(ComponentIds.FocusPoint, component);
        }

        public Entity ReplaceFocusPoint(UnityEngine.Vector3 newDeltaPosition) {
            var previousComponent = hasFocusPoint ? focusPoint : null;
            var component = _focusPointComponentPool.Count > 0 ? _focusPointComponentPool.Pop() : new Assets.FocusPointComponent();
            component.Position = newDeltaPosition;
            ReplaceComponent(ComponentIds.FocusPoint, component);
            if (previousComponent != null) {
                _focusPointComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveFocusPoint() {
            var component = focusPoint;
            RemoveComponent(ComponentIds.FocusPoint);
            _focusPointComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherFocusPoint;

        public static IMatcher FocusPoint {
            get {
                if (_matcherFocusPoint == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.FocusPoint);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherFocusPoint = matcher;
                }

                return _matcherFocusPoint;
            }
        }
    }
}
