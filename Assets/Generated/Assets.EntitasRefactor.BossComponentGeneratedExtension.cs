using Assets;

namespace Entitas {
    public partial class Entity {
        static readonly BossComponent bossComponent = new BossComponent();

        public bool isBoss {
            get { return HasComponent(ComponentIds.Boss); }
            set {
                if (value != isBoss) {
                    if (value) {
                        AddComponent(ComponentIds.Boss, bossComponent);
                    } else {
                        RemoveComponent(ComponentIds.Boss);
                    }
                }
            }
        }

        public Entity IsBoss(bool value) {
            isBoss = value;
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherBoss;

        public static IMatcher Boss {
            get {
                if (_matcherBoss == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Boss);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherBoss = matcher;
                }

                return _matcherBoss;
            }
        }
    }
}
