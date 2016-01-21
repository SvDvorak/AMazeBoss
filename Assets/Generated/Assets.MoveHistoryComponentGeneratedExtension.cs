using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public Assets.MoveHistoryComponent moveHistory { get { return (Assets.MoveHistoryComponent)GetComponent(ComponentIds.MoveHistory); } }

        public bool hasMoveHistory { get { return HasComponent(ComponentIds.MoveHistory); } }

        static readonly Stack<Assets.MoveHistoryComponent> _moveHistoryComponentPool = new Stack<Assets.MoveHistoryComponent>();

        public static void ClearMoveHistoryComponentPool() {
            _moveHistoryComponentPool.Clear();
        }

        public Entity AddMoveHistory(System.Collections.Generic.List<Assets.TilePos> newValue) {
            var component = _moveHistoryComponentPool.Count > 0 ? _moveHistoryComponentPool.Pop() : new Assets.MoveHistoryComponent();
            component.Value = newValue;
            return AddComponent(ComponentIds.MoveHistory, component);
        }

        public Entity ReplaceMoveHistory(System.Collections.Generic.List<Assets.TilePos> newValue) {
            var previousComponent = hasMoveHistory ? moveHistory : null;
            var component = _moveHistoryComponentPool.Count > 0 ? _moveHistoryComponentPool.Pop() : new Assets.MoveHistoryComponent();
            component.Value = newValue;
            ReplaceComponent(ComponentIds.MoveHistory, component);
            if (previousComponent != null) {
                _moveHistoryComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveMoveHistory() {
            var component = moveHistory;
            RemoveComponent(ComponentIds.MoveHistory);
            _moveHistoryComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherMoveHistory;

        public static IMatcher MoveHistory {
            get {
                if (_matcherMoveHistory == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.MoveHistory);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherMoveHistory = matcher;
                }

                return _matcherMoveHistory;
            }
        }
    }
}
