namespace Entitas {
    public partial class Entity {
        static readonly Assets.BossSprintingComponent bossSprintingComponent = new Assets.BossSprintingComponent();

        public bool isBossSprinting {
            get { return HasComponent(ComponentIds.BossSprinting); }
            set {
                if (value != isBossSprinting) {
                    if (value) {
                        AddComponent(ComponentIds.BossSprinting, bossSprintingComponent);
                    } else {
                        RemoveComponent(ComponentIds.BossSprinting);
                    }
                }
            }
        }

        public Entity IsBossSprinting(bool value) {
            isBossSprinting = value;
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherBossSprinting;

        public static IMatcher BossSprinting {
            get {
                if (_matcherBossSprinting == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.BossSprinting);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherBossSprinting = matcher;
                }

                return _matcherBossSprinting;
            }
        }
    }
}
