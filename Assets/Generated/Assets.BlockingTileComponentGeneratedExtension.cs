using Entitas;

namespace Entitas {
    public partial class Entity {
        static readonly Assets.BlockingTileComponent blockingTileComponent = new Assets.BlockingTileComponent();

        public bool isBlockingTile {
            get { return HasComponent(GameComponentIds.BlockingTile); }
            set {
                if (value != isBlockingTile) {
                    if (value) {
                        AddComponent(GameComponentIds.BlockingTile, blockingTileComponent);
                    } else {
                        RemoveComponent(GameComponentIds.BlockingTile);
                    }
                }
            }
        }

        public Entity IsBlockingTile(bool value) {
            isBlockingTile = value;
            return this;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherBlockingTile;

        public static IMatcher BlockingTile {
            get {
                if (_matcherBlockingTile == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.BlockingTile);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherBlockingTile = matcher;
                }

                return _matcherBlockingTile;
            }
        }
    }
