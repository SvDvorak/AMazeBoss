namespace Entitas {
    public partial class Entity {
        static readonly Assets.TrapActivatedComponent trapActivatedComponent = new Assets.TrapActivatedComponent();

        public bool isTrapActivated {
            get { return HasComponent(ComponentIds.TrapActivated); }
            set {
                if (value != isTrapActivated) {
                    if (value) {
                        AddComponent(ComponentIds.TrapActivated, trapActivatedComponent);
                    } else {
                        RemoveComponent(ComponentIds.TrapActivated);
                    }
                }
            }
        }

        public Entity IsTrapActivated(bool value) {
            isTrapActivated = value;
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherTrapActivated;

        public static IMatcher TrapActivated {
            get {
                if (_matcherTrapActivated == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.TrapActivated);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherTrapActivated = matcher;
                }

                return _matcherTrapActivated;
            }
        }
    }
}
