namespace Entitas {
    public partial class Entity {
        static readonly Assets.ActiveTurnComponent activeTurnComponent = new Assets.ActiveTurnComponent();

        public bool isActiveTurn {
            get { return HasComponent(ComponentIds.ActiveTurn); }
            set {
                if (value != isActiveTurn) {
                    if (value) {
                        AddComponent(ComponentIds.ActiveTurn, activeTurnComponent);
                    } else {
                        RemoveComponent(ComponentIds.ActiveTurn);
                    }
                }
            }
        }

        public Entity IsActiveTurn(bool value) {
            isActiveTurn = value;
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherActiveTurn;

        public static IMatcher ActiveTurn {
            get {
                if (_matcherActiveTurn == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.ActiveTurn);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherActiveTurn = matcher;
                }

                return _matcherActiveTurn;
            }
        }
    }
}
