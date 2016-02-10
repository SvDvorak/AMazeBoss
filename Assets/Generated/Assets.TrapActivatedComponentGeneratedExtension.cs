using Entitas;

namespace Entitas {
    public partial class Entity {
        static readonly Assets.TrapActivatedComponent trapActivatedComponent = new Assets.TrapActivatedComponent();

        public bool isTrapActivated {
            get { return HasComponent(GameComponentIds.TrapActivated); }
            set {
                if (value != isTrapActivated) {
                    if (value) {
                        AddComponent(GameComponentIds.TrapActivated, trapActivatedComponent);
                    } else {
                        RemoveComponent(GameComponentIds.TrapActivated);
                    }
                }
            }
        }

        public Entity IsTrapActivated(bool value) {
            isTrapActivated = value;
            return this;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherTrapActivated;

        public static IMatcher TrapActivated {
            get {
                if (_matcherTrapActivated == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.TrapActivated);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherTrapActivated = matcher;
                }

                return _matcherTrapActivated;
            }
        }
    }
