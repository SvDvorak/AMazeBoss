namespace Entitas {
    public partial class Entity {
        static readonly Assets.ItemComponent itemComponent = new Assets.ItemComponent();

        public bool isItem {
            get { return HasComponent(ComponentIds.Item); }
            set {
                if (value != isItem) {
                    if (value) {
                        AddComponent(ComponentIds.Item, itemComponent);
                    } else {
                        RemoveComponent(ComponentIds.Item);
                    }
                }
            }
        }

        public Entity IsItem(bool value) {
            isItem = value;
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherItem;

        public static IMatcher Item {
            get {
                if (_matcherItem == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Item);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherItem = matcher;
                }

                return _matcherItem;
            }
        }
    }
}
