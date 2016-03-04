using Entitas;

namespace Entitas {
    public partial class Entity {
        static readonly Assets.ExitGateComponent exitGateComponent = new Assets.ExitGateComponent();

        public bool isExitGate {
            get { return HasComponent(GameComponentIds.ExitGate); }
            set {
                if (value != isExitGate) {
                    if (value) {
                        AddComponent(GameComponentIds.ExitGate, exitGateComponent);
                    } else {
                        RemoveComponent(GameComponentIds.ExitGate);
                    }
                }
            }
        }

        public Entity IsExitGate(bool value) {
            isExitGate = value;
            return this;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherExitGate;

        public static IMatcher ExitGate {
            get {
                if (_matcherExitGate == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.ExitGate);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherExitGate = matcher;
                }

                return _matcherExitGate;
            }
        }
    }
