using System.Collections.Generic;

using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.QueuedPositionComponent queuedPosition { get { return (Assets.QueuedPositionComponent)GetComponent(GameComponentIds.QueuedPosition); } }

        public bool hasQueuedPosition { get { return HasComponent(GameComponentIds.QueuedPosition); } }

        static readonly Stack<Assets.QueuedPositionComponent> _queuedPositionComponentPool = new Stack<Assets.QueuedPositionComponent>();

        public static void ClearQueuedPositionComponentPool() {
            _queuedPositionComponentPool.Clear();
        }

        public Entity AddQueuedPosition(Assets.TilePos newValue) {
            var component = _queuedPositionComponentPool.Count > 0 ? _queuedPositionComponentPool.Pop() : new Assets.QueuedPositionComponent();
            component.Value = newValue;
            return AddComponent(GameComponentIds.QueuedPosition, component);
        }

        public Entity ReplaceQueuedPosition(Assets.TilePos newValue) {
            var previousComponent = hasQueuedPosition ? queuedPosition : null;
            var component = _queuedPositionComponentPool.Count > 0 ? _queuedPositionComponentPool.Pop() : new Assets.QueuedPositionComponent();
            component.Value = newValue;
            ReplaceComponent(GameComponentIds.QueuedPosition, component);
            if (previousComponent != null) {
                _queuedPositionComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveQueuedPosition() {
            var component = queuedPosition;
            RemoveComponent(GameComponentIds.QueuedPosition);
            _queuedPositionComponentPool.Push(component);
            return this;
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
