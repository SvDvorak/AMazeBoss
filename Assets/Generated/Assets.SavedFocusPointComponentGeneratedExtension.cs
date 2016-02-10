using System.Collections.Generic;

using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.SavedFocusPointComponent savedFocusPoint { get { return (Assets.SavedFocusPointComponent)GetComponent(GameComponentIds.SavedFocusPoint); } }

        public bool hasSavedFocusPoint { get { return HasComponent(GameComponentIds.SavedFocusPoint); } }

        static readonly Stack<Assets.SavedFocusPointComponent> _savedFocusPointComponentPool = new Stack<Assets.SavedFocusPointComponent>();

        public static void ClearSavedFocusPointComponentPool() {
            _savedFocusPointComponentPool.Clear();
        }

        public Entity AddSavedFocusPoint(UnityEngine.Vector3 newPosition) {
            var component = _savedFocusPointComponentPool.Count > 0 ? _savedFocusPointComponentPool.Pop() : new Assets.SavedFocusPointComponent();
            component.Position = newPosition;
            return AddComponent(GameComponentIds.SavedFocusPoint, component);
        }

        public Entity ReplaceSavedFocusPoint(UnityEngine.Vector3 newPosition) {
            var previousComponent = hasSavedFocusPoint ? savedFocusPoint : null;
            var component = _savedFocusPointComponentPool.Count > 0 ? _savedFocusPointComponentPool.Pop() : new Assets.SavedFocusPointComponent();
            component.Position = newPosition;
            ReplaceComponent(GameComponentIds.SavedFocusPoint, component);
            if (previousComponent != null) {
                _savedFocusPointComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveSavedFocusPoint() {
            var component = savedFocusPoint;
            RemoveComponent(GameComponentIds.SavedFocusPoint);
            _savedFocusPointComponentPool.Push(component);
            return this;
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
