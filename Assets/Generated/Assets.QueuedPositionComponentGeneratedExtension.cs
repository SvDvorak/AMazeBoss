using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.QueuedPositionComponent queuedPosition { get { return (Assets.QueuedPositionComponent)GetComponent(GameComponentIds.QueuedPosition); } }

        public bool hasQueuedPosition { get { return HasComponent(GameComponentIds.QueuedPosition); } }

        public Entity AddQueuedPosition(Assets.TilePos newValue) {
            var componentPool = GetComponentPool(GameComponentIds.QueuedPosition);
            var component = (Assets.QueuedPositionComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.QueuedPositionComponent());
            component.Value = newValue;
            return AddComponent(GameComponentIds.QueuedPosition, component);
        }

        public Entity ReplaceQueuedPosition(Assets.TilePos newValue) {
            var componentPool = GetComponentPool(GameComponentIds.QueuedPosition);
            var component = (Assets.QueuedPositionComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.QueuedPositionComponent());
            component.Value = newValue;
            ReplaceComponent(GameComponentIds.QueuedPosition, component);
            return this;
        }

        public Entity RemoveQueuedPosition() {
            return RemoveComponent(GameComponentIds.QueuedPosition);;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherQueuedPosition;

        public static IMatcher QueuedPosition {
            get {
                if (_matcherQueuedPosition == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.QueuedPosition);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherQueuedPosition = matcher;
                }

                return _matcherQueuedPosition;
            }
        }
    }
