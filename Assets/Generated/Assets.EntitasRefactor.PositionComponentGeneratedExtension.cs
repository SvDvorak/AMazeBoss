using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public Assets.EntitasRefactor.PositionComponent position { get { return (Assets.EntitasRefactor.PositionComponent)GetComponent(ComponentIds.Position); } }

        public bool hasPosition { get { return HasComponent(ComponentIds.Position); } }

        static readonly Stack<Assets.EntitasRefactor.PositionComponent> _positionComponentPool = new Stack<Assets.EntitasRefactor.PositionComponent>();

        public static void ClearPositionComponentPool() {
            _positionComponentPool.Clear();
        }

        public Entity AddPosition(TilePos newValue) {
            var component = _positionComponentPool.Count > 0 ? _positionComponentPool.Pop() : new Assets.EntitasRefactor.PositionComponent();
            component.Value = newValue;
            return AddComponent(ComponentIds.Position, component);
        }

        public Entity ReplacePosition(TilePos newValue) {
            var previousComponent = hasPosition ? position : null;
            var component = _positionComponentPool.Count > 0 ? _positionComponentPool.Pop() : new Assets.EntitasRefactor.PositionComponent();
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
