namespace Entitas {
    public partial class Entity {
        static readonly Assets.DeadComponent deadComponent = new Assets.DeadComponent();

        public bool isDead {
            get { return HasComponent(ComponentIds.Dead); }
            set {
                if (value != isDead) {
                    if (value) {
                        AddComponent(ComponentIds.Dead, deadComponent);
                    } else {
                        RemoveComponent(ComponentIds.Dead);
                    }
                }
            }
        }

        public Entity IsDead(bool value) {
            isDead = value;
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherDead;

        public static IMatcher Dead {
            get {
                if (_matcherDead == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Dead);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherDead = matcher;
                }

                return _matcherDead;
            }
        }
    }
}
