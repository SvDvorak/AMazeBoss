using Entitas;

namespace Entitas {
    public partial class Entity {
        static readonly Assets.TileComponent tileComponent = new Assets.TileComponent();

        public bool isTile {
            get { return HasComponent(GameComponentIds.Tile); }
            set {
                if (value != isTile) {
                    if (value) {
                        AddComponent(GameComponentIds.Tile, tileComponent);
                    } else {
                        RemoveComponent(GameComponentIds.Tile);
                    }
                }
            }
        }

        public Entity IsTile(bool value) {
            isTile = value;
            return this;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherTile;

        public static IMatcher Tile {
            get {
                if (_matcherTile == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.Tile);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherTile = matcher;
                }

                return _matcherTile;
            }
        }
    }
