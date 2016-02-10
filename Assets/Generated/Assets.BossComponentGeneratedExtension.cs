using Entitas;

namespace Entitas {
    public partial class Entity {
        static readonly Assets.BossComponent bossComponent = new Assets.BossComponent();

        public bool isBoss {
            get { return HasComponent(GameComponentIds.Boss); }
            set {
                if (value != isBoss) {
                    if (value) {
                        AddComponent(GameComponentIds.Boss, bossComponent);
                    } else {
                        RemoveComponent(GameComponentIds.Boss);
                    }
                }
            }
        }

        public Entity IsBoss(bool value) {
            isBoss = value;
            return this;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherBoss;

        public static IMatcher Boss {
            get {
                if (_matcherBoss == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.Boss);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherBoss = matcher;
                }

                return _matcherBoss;
            }
        }
    }
