using Entitas;

namespace Entitas {
    public partial class Entity {
        static readonly Assets.RockedComponent rockedComponent = new Assets.RockedComponent();

        public bool isRocked {
            get { return HasComponent(GameComponentIds.Rocked); }
            set {
                if (value != isRocked) {
                    if (value) {
                        AddComponent(GameComponentIds.Rocked, rockedComponent);
                    } else {
                        RemoveComponent(GameComponentIds.Rocked);
                    }
                }
            }
        }

        public Entity IsRocked(bool value) {
            isRocked = value;
            return this;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherRocked;

        public static IMatcher Rocked {
            get {
                if (_matcherRocked == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.Rocked);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherRocked = matcher;
                }

                return _matcherRocked;
            }
        }
    }
