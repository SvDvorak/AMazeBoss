using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.PositionComponent position { get { return (Assets.PositionComponent)GetComponent(GameComponentIds.Position); } }

        public bool hasPosition { get { return HasComponent(GameComponentIds.Position); } }

        public Entity AddPosition(Assets.TilePos newValue) {
            var componentPool = GetComponentPool(GameComponentIds.Position);
            var component = (Assets.PositionComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.PositionComponent());
            component.Value = newValue;
            return AddComponent(GameComponentIds.Position, component);
        }

        public Entity ReplacePosition(Assets.TilePos newValue) {
            var componentPool = GetComponentPool(GameComponentIds.Position);
            var component = (Assets.PositionComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.PositionComponent());
            component.Value = newValue;
            ReplaceComponent(GameComponentIds.Position, component);
            return this;
        }

        public Entity RemovePosition() {
            return RemoveComponent(GameComponentIds.Position);;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherPosition;

        public static IMatcher Position {
            get {
                if (_matcherPosition == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.Position);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherPosition = matcher;
                }

                return _matcherPosition;
            }
        }
    }
