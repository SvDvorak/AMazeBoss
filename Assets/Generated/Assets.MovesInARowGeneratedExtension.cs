using System.Collections.Generic;

using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.MovesInARow movesInARow { get { return (Assets.MovesInARow)GetComponent(GameComponentIds.MovesInARow); } }

        public bool hasMovesInARow { get { return HasComponent(GameComponentIds.MovesInARow); } }

        static readonly Stack<Assets.MovesInARow> _movesInARowComponentPool = new Stack<Assets.MovesInARow>();

        public static void ClearMovesInARowComponentPool() {
            _movesInARowComponentPool.Clear();
        }

        public Entity AddMovesInARow(int newMoves) {
            var component = _movesInARowComponentPool.Count > 0 ? _movesInARowComponentPool.Pop() : new Assets.MovesInARow();
            component.Moves = newMoves;
            return AddComponent(GameComponentIds.MovesInARow, component);
        }

        public Entity ReplaceMovesInARow(int newMoves) {
            var previousComponent = hasMovesInARow ? movesInARow : null;
            var component = _movesInARowComponentPool.Count > 0 ? _movesInARowComponentPool.Pop() : new Assets.MovesInARow();
            component.Moves = newMoves;
            ReplaceComponent(GameComponentIds.MovesInARow, component);
            if (previousComponent != null) {
                _movesInARowComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveMovesInARow() {
            var component = movesInARow;
            RemoveComponent(GameComponentIds.MovesInARow);
            _movesInARowComponentPool.Push(component);
            return this;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherMovesInARow;

        public static IMatcher MovesInARow {
            get {
                if (_matcherMovesInARow == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.MovesInARow);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherMovesInARow = matcher;
                }

                return _matcherMovesInARow;
            }
        }
    }
