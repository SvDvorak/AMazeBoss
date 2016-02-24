using Entitas;

namespace Entitas {
    public partial class Entity {
        static readonly Assets.PuzzleEdgeComponent puzzleEdgeComponent = new Assets.PuzzleEdgeComponent();

        public bool isPuzzleEdge {
            get { return HasComponent(GameComponentIds.PuzzleEdge); }
            set {
                if (value != isPuzzleEdge) {
                    if (value) {
                        AddComponent(GameComponentIds.PuzzleEdge, puzzleEdgeComponent);
                    } else {
                        RemoveComponent(GameComponentIds.PuzzleEdge);
                    }
                }
            }
        }

        public Entity IsPuzzleEdge(bool value) {
            isPuzzleEdge = value;
            return this;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherPuzzleEdge;

        public static IMatcher PuzzleEdge {
            get {
                if (_matcherPuzzleEdge == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.PuzzleEdge);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherPuzzleEdge = matcher;
                }

                return _matcherPuzzleEdge;
            }
        }
    }
