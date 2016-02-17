using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.MovesInARow movesInARow { get { return (Assets.MovesInARow)GetComponent(GameComponentIds.MovesInARow); } }

        public bool hasMovesInARow { get { return HasComponent(GameComponentIds.MovesInARow); } }

        public Entity AddMovesInARow(int newMoves) {
            var componentPool = GetComponentPool(GameComponentIds.MovesInARow);
            var component = (Assets.MovesInARow)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.MovesInARow());
            component.Moves = newMoves;
            return AddComponent(GameComponentIds.MovesInARow, component);
        }

        public Entity ReplaceMovesInARow(int newMoves) {
            var componentPool = GetComponentPool(GameComponentIds.MovesInARow);
            var component = (Assets.MovesInARow)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.MovesInARow());
            component.Moves = newMoves;
            ReplaceComponent(GameComponentIds.MovesInARow, component);
            return this;
        }

        public Entity RemoveMovesInARow() {
            return RemoveComponent(GameComponentIds.MovesInARow);;
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
