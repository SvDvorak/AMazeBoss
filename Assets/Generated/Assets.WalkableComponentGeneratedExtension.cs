namespace Entitas {
    public partial class Entity {
        static readonly Assets.BlockingTileComponent blockingTileComponent = new Assets.BlockingTileComponent();

        public bool isBlockingTile {
            get { return HasComponent(ComponentIds.Walkable); }
            set {
                if (value != isBlockingTile) {
                    if (value) {
                        AddComponent(ComponentIds.Walkable, blockingTileComponent);
                    } else {
                        RemoveComponent(ComponentIds.Walkable);
                    }
                }
            }
        }

        public Entity IsBlockingTile(bool value) {
            isBlockingTile = value;
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherWalkable;

        public static IMatcher Walkable {
            get {
                if (_matcherWalkable == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Walkable);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherWalkable = matcher;
                }

                return _matcherWalkable;
            }
        }
    }
}
