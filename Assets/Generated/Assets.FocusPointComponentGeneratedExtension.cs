using System.Collections.Generic;

using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.FocusPointComponent focusPoint { get { return (Assets.FocusPointComponent)GetComponent(GameComponentIds.FocusPoint); } }

        public bool hasFocusPoint { get { return HasComponent(GameComponentIds.FocusPoint); } }

        static readonly Stack<Assets.FocusPointComponent> _focusPointComponentPool = new Stack<Assets.FocusPointComponent>();

        public static void ClearFocusPointComponentPool() {
            _focusPointComponentPool.Clear();
        }

        public Entity AddFocusPoint(UnityEngine.Vector3 newPosition) {
            var component = _focusPointComponentPool.Count > 0 ? _focusPointComponentPool.Pop() : new Assets.FocusPointComponent();
            component.Position = newPosition;
            return AddComponent(GameComponentIds.FocusPoint, component);
        }

        public Entity ReplaceFocusPoint(UnityEngine.Vector3 newPosition) {
            var previousComponent = hasFocusPoint ? focusPoint : null;
            var component = _focusPointComponentPool.Count > 0 ? _focusPointComponentPool.Pop() : new Assets.FocusPointComponent();
            component.Position = newPosition;
            ReplaceComponent(GameComponentIds.FocusPoint, component);
            if (previousComponent != null) {
                _focusPointComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveFocusPoint() {
            var component = focusPoint;
            RemoveComponent(GameComponentIds.FocusPoint);
            _focusPointComponentPool.Push(component);
            return this;
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
