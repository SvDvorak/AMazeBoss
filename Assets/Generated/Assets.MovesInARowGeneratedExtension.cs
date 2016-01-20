using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public Assets.MovesInARow movesInARow { get { return (Assets.MovesInARow)GetComponent(ComponentIds.MovesInARow); } }

        public bool hasMovesInARow { get { return HasComponent(ComponentIds.MovesInARow); } }

        static readonly Stack<Assets.MovesInARow> _movesInARowComponentPool = new Stack<Assets.MovesInARow>();

        public static void ClearMovesInARowComponentPool() {
            _movesInARowComponentPool.Clear();
        }

        public Entity AddMovesInARow(int newMoves) {
            var component = _movesInARowComponentPool.Count > 0 ? _movesInARowComponentPool.Pop() : new Assets.MovesInARow();
            component.Moves = newMoves;
            return AddComponent(ComponentIds.MovesInARow, component);
        }

        public Entity ReplaceMovesInARow(int newMoves) {
            var previousComponent = hasMovesInARow ? movesInARow : null;
            var component = _movesInARowComponentPool.Count > 0 ? _movesInARowComponentPool.Pop() : new Assets.MovesInARow();
            component.Moves = newMoves;
            ReplaceComponent(ComponentIds.MovesInARow, component);
            if (previousComponent != null) {
                _movesInARowComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveMovesInARow() {
            var component = movesInARow;
            RemoveComponent(ComponentIds.MovesInARow);
            _movesInARowComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherMovesInARow;

        public static IMatcher MovesInARow {
            get {
                if (_matcherMovesInARow == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.MovesInARow);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherMovesInARow = matcher;
                }

                return _matcherMovesInARow;
            }
        }
    }
}
