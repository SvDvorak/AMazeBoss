using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public Assets.PositionComponent position { get { return (Assets.PositionComponent)GetComponent(ComponentIds.Position); } }

        public bool hasPosition { get { return HasComponent(ComponentIds.Position); } }

        static readonly Stack<Assets.PositionComponent> _positionComponentPool = new Stack<Assets.PositionComponent>();

        public static void ClearPositionComponentPool() {
            _positionComponentPool.Clear();
        }

        public Entity AddPosition(Assets.TilePos newValue) {
            var component = _positionComponentPool.Count > 0 ? _positionComponentPool.Pop() : new Assets.PositionComponent();
            component.Value = newValue;
            return AddComponent(ComponentIds.Position, component);
        }

        public Entity ReplacePosition(Assets.TilePos newValue) {
            var previousComponent = hasPosition ? position : null;
            var component = _positionComponentPool.Count > 0 ? _positionComponentPool.Pop() : new Assets.PositionComponent();
            component.Value = newValue;
            ReplaceComponent(ComponentIds.Position, component);
            if (previousComponent != null) {
                _positionComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemovePosition() {
            var component = position;
            RemoveComponent(ComponentIds.Position);
            _positionComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherPosition;

        public static IMatcher Position {
            get {
                if (_matcherPosition == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Position);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherPosition = matcher;
                }

                return _matcherPosition;
            }
        }
    }
}
