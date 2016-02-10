using Entitas;

namespace Entitas {
    public partial class Entity {
        static readonly Assets.ItemComponent itemComponent = new Assets.ItemComponent();

        public bool isItem {
            get { return HasComponent(GameComponentIds.Item); }
            set {
                if (value != isItem) {
                    if (value) {
                        AddComponent(GameComponentIds.Item, itemComponent);
                    } else {
                        RemoveComponent(GameComponentIds.Item);
                    }
                }
            }
        }

        public Entity IsItem(bool value) {
            isItem = value;
            return this;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherItem;

        public static IMatcher Item {
            get {
                if (_matcherItem == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.Item);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherItem = matcher;
                }

                return _matcherItem;
            }
        }
    }
