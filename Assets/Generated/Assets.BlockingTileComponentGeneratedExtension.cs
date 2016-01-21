namespace Entitas {
    public partial class Entity {
        static readonly Assets.BlockingTileComponent blockingTileComponent = new Assets.BlockingTileComponent();

        public bool isBlockingTile {
            get { return HasComponent(ComponentIds.BlockingTile); }
            set {
                if (value != isBlockingTile) {
                    if (value) {
                        AddComponent(ComponentIds.BlockingTile, blockingTileComponent);
                    } else {
                        RemoveComponent(ComponentIds.BlockingTile);
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
        static IMatcher _matcherBlockingTile;

        public static IMatcher BlockingTile {
            get {
                if (_matcherBlockingTile == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.BlockingTile);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherBlockingTile = matcher;
                }

                return _matcherBlockingTile;
            }
        }
    }
}
