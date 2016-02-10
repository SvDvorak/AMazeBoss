using Entitas;

namespace Entitas {
    public partial class Entity {
        static readonly Assets.BoxComponent boxComponent = new Assets.BoxComponent();

        public bool isBox {
            get { return HasComponent(GameComponentIds.Box); }
            set {
                if (value != isBox) {
                    if (value) {
                        AddComponent(GameComponentIds.Box, boxComponent);
                    } else {
                        RemoveComponent(GameComponentIds.Box);
                    }
                }
            }
        }

        public Entity IsBox(bool value) {
            isBox = value;
            return this;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherBox;

        public static IMatcher Box {
            get {
                if (_matcherBox == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.Box);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherBox = matcher;
                }

                return _matcherBox;
            }
        }
    }
