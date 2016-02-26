using Entitas;

namespace Entitas {
    public partial class Entity {
        static readonly Assets.PuzzleAreaComponent puzzleAreaComponent = new Assets.PuzzleAreaComponent();

        public bool isPuzzleArea {
            get { return HasComponent(GameComponentIds.PuzzleArea); }
            set {
                if (value != isPuzzleArea) {
                    if (value) {
                        AddComponent(GameComponentIds.PuzzleArea, puzzleAreaComponent);
                    } else {
                        RemoveComponent(GameComponentIds.PuzzleArea);
                    }
                }
            }
        }

        public Entity IsPuzzleArea(bool value) {
            isPuzzleArea = value;
            return this;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherPuzzleArea;

        public static IMatcher PuzzleArea {
            get {
                if (_matcherPuzzleArea == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.PuzzleArea);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherPuzzleArea = matcher;
                }

                return _matcherPuzzleArea;
            }
        }
    }
