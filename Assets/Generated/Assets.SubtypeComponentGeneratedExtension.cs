using System.Collections.Generic;

using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.SubtypeComponent subtype { get { return (Assets.SubtypeComponent)GetComponent(GameComponentIds.Subtype); } }

        public bool hasSubtype { get { return HasComponent(GameComponentIds.Subtype); } }

        static readonly Stack<Assets.SubtypeComponent> _subtypeComponentPool = new Stack<Assets.SubtypeComponent>();

        public static void ClearSubtypeComponentPool() {
            _subtypeComponentPool.Clear();
        }

        public Entity AddSubtype(string newValue) {
            var component = _subtypeComponentPool.Count > 0 ? _subtypeComponentPool.Pop() : new Assets.SubtypeComponent();
            component.Value = newValue;
            return AddComponent(GameComponentIds.Subtype, component);
        }

        public Entity ReplaceSubtype(string newValue) {
            var previousComponent = hasSubtype ? subtype : null;
            var component = _subtypeComponentPool.Count > 0 ? _subtypeComponentPool.Pop() : new Assets.SubtypeComponent();
            component.Value = newValue;
            ReplaceComponent(GameComponentIds.Subtype, component);
            if (previousComponent != null) {
                _subtypeComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveSubtype() {
            var component = subtype;
            RemoveComponent(GameComponentIds.Subtype);
            _subtypeComponentPool.Push(component);
            return this;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherSubtype;

        public static IMatcher Subtype {
            get {
                if (_matcherSubtype == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.Subtype);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherSubtype = matcher;
                }

                return _matcherSubtype;
            }
        }
    }
