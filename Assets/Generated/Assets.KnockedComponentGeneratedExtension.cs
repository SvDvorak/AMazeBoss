using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public Assets.KnockedComponent knocked { get { return (Assets.KnockedComponent)GetComponent(ComponentIds.Knocked); } }

        public bool hasKnocked { get { return HasComponent(ComponentIds.Knocked); } }

        static readonly Stack<Assets.KnockedComponent> _knockedComponentPool = new Stack<Assets.KnockedComponent>();

        public static void ClearKnockedComponentPool() {
            _knockedComponentPool.Clear();
        }

        public Entity AddKnocked(Assets.TilePos newFromDirection, bool newImmediate) {
            var component = _knockedComponentPool.Count > 0 ? _knockedComponentPool.Pop() : new Assets.KnockedComponent();
            component.FromDirection = newFromDirection;
            component.Immediate = newImmediate;
            return AddComponent(ComponentIds.Knocked, component);
        }

        public Entity ReplaceKnocked(Assets.TilePos newFromDirection, bool newImmediate) {
            var previousComponent = hasKnocked ? knocked : null;
            var component = _knockedComponentPool.Count > 0 ? _knockedComponentPool.Pop() : new Assets.KnockedComponent();
            component.FromDirection = newFromDirection;
            component.Immediate = newImmediate;
            ReplaceComponent(ComponentIds.Knocked, component);
            if (previousComponent != null) {
                _knockedComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveKnocked() {
            var component = knocked;
            RemoveComponent(ComponentIds.Knocked);
            _knockedComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherKnocked;

        public static IMatcher Knocked {
            get {
                if (_matcherKnocked == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Knocked);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherKnocked = matcher;
                }

                return _matcherKnocked;
            }
        }
    }
}
