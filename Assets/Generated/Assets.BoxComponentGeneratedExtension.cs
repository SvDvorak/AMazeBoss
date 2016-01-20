namespace Entitas {
    public partial class Entity {
        static readonly Assets.BoxComponent boxComponent = new Assets.BoxComponent();

        public bool isBox {
            get { return HasComponent(ComponentIds.Box); }
            set {
                if (value != isBox) {
                    if (value) {
                        AddComponent(ComponentIds.Box, boxComponent);
                    } else {
                        RemoveComponent(ComponentIds.Box);
                    }
                }
            }
        }

        public Entity IsBox(bool value) {
            isBox = value;
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherBox;

        public static IMatcher Box {
            get {
                if (_matcherBox == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Box);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherBox = matcher;
                }

                return _matcherBox;
            }
        }
    }
}
