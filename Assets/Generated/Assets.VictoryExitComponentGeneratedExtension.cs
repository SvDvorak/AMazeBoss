using Entitas;

namespace Entitas {
    public partial class Entity {
        static readonly Assets.VictoryExitComponent victoryExitComponent = new Assets.VictoryExitComponent();

        public bool isVictoryExit {
            get { return HasComponent(GameComponentIds.VictoryExit); }
            set {
                if (value != isVictoryExit) {
                    if (value) {
                        AddComponent(GameComponentIds.VictoryExit, victoryExitComponent);
                    } else {
                        RemoveComponent(GameComponentIds.VictoryExit);
                    }
                }
            }
        }

        public Entity IsVictoryExit(bool value) {
            isVictoryExit = value;
            return this;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherVictoryExit;

        public static IMatcher VictoryExit {
            get {
                if (_matcherVictoryExit == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.VictoryExit);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherVictoryExit = matcher;
                }

                return _matcherVictoryExit;
            }
        }
    }
