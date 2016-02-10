using Entitas;

namespace Entitas {
    public partial class Entity {
        static readonly Assets.ActiveTurnComponent activeTurnComponent = new Assets.ActiveTurnComponent();

        public bool isActiveTurn {
            get { return HasComponent(GameComponentIds.ActiveTurn); }
            set {
                if (value != isActiveTurn) {
                    if (value) {
                        AddComponent(GameComponentIds.ActiveTurn, activeTurnComponent);
                    } else {
                        RemoveComponent(GameComponentIds.ActiveTurn);
                    }
                }
            }
        }

        public Entity IsActiveTurn(bool value) {
            isActiveTurn = value;
            return this;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherActiveTurn;

        public static IMatcher ActiveTurn {
            get {
                if (_matcherActiveTurn == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.ActiveTurn);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherActiveTurn = matcher;
                }

                return _matcherActiveTurn;
            }
        }
    }
