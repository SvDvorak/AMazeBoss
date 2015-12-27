namespace Entitas {
    public partial class Entity {
        static readonly Assets.EntitasRefactor.TileComponent tileComponent = new Assets.EntitasRefactor.TileComponent();

        public bool isTile {
            get { return HasComponent(ComponentIds.Tile); }
            set {
                if (value != isTile) {
                    if (value) {
                        AddComponent(ComponentIds.Tile, tileComponent);
                    } else {
                        RemoveComponent(ComponentIds.Tile);
                    }
                }
            }
        }

        public Entity IsTile(bool value) {
            isTile = value;
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherTile;

        public static IMatcher Tile {
            get {
                if (_matcherTile == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Tile);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherTile = matcher;
                }

                return _matcherTile;
            }
        }
    }
}
