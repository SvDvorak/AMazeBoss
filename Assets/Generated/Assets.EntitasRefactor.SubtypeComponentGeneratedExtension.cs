using System.Collections.Generic;
using Assets;

namespace Entitas {
    public partial class Entity {
        public SubtypeComponent subtype { get { return (SubtypeComponent)GetComponent(ComponentIds.Subtype); } }

        public bool hasSubtype { get { return HasComponent(ComponentIds.Subtype); } }

        static readonly Stack<SubtypeComponent> _subtypeComponentPool = new Stack<SubtypeComponent>();

        public static void ClearSubtypeComponentPool() {
            _subtypeComponentPool.Clear();
        }

        public Entity AddSubtype(string newValue) {
            var component = _subtypeComponentPool.Count > 0 ? _subtypeComponentPool.Pop() : new SubtypeComponent();
            component.Value = newValue;
            return AddComponent(ComponentIds.Subtype, component);
        }

        public Entity ReplaceSubtype(string newValue) {
            var previousComponent = hasSubtype ? subtype : null;
            var component = _subtypeComponentPool.Count > 0 ? _subtypeComponentPool.Pop() : new SubtypeComponent();
            component.Value = newValue;
            ReplaceComponent(ComponentIds.Subtype, component);
            if (previousComponent != null) {
                _subtypeComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveSubtype() {
            var component = subtype;
            RemoveComponent(ComponentIds.Subtype);
            _subtypeComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherSubtype;

        public static IMatcher Subtype {
            get {
                if (_matcherSubtype == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Subtype);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherSubtype = matcher;
                }

                return _matcherSubtype;
            }
        }
    }
}
