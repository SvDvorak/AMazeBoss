using Entitas;

namespace Entitas {
    public partial class Entity {
        static readonly Assets.DeadComponent deadComponent = new Assets.DeadComponent();

        public bool isDead {
            get { return HasComponent(GameComponentIds.Dead); }
            set {
                if (value != isDead) {
                    if (value) {
                        AddComponent(GameComponentIds.Dead, deadComponent);
                    } else {
                        RemoveComponent(GameComponentIds.Dead);
                    }
                }
            }
        }

        public Entity IsDead(bool value) {
            isDead = value;
            return this;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherDead;

        public static IMatcher Dead {
            get {
                if (_matcherDead == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.Dead);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherDead = matcher;
                }

                return _matcherDead;
            }
        }
    }
