using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public Assets.EntitasRefactor.TileComponent tile { get { return (Assets.EntitasRefactor.TileComponent)GetComponent(ComponentIds.Tile); } }

        public bool hasTile { get { return HasComponent(ComponentIds.Tile); } }

        static readonly Stack<Assets.EntitasRefactor.TileComponent> _tileComponentPool = new Stack<Assets.EntitasRefactor.TileComponent>();

        public static void ClearTileComponentPool() {
            _tileComponentPool.Clear();
        }

        public Entity AddTile(Assets.MainTileType newType) {
            var component = _tileComponentPool.Count > 0 ? _tileComponentPool.Pop() : new Assets.EntitasRefactor.TileComponent();
            component.Type = newType;
            return AddComponent(ComponentIds.Tile, component);
        }

        public Entity ReplaceTile(Assets.MainTileType newType) {
            var previousComponent = hasTile ? tile : null;
            var component = _tileComponentPool.Count > 0 ? _tileComponentPool.Pop() : new Assets.EntitasRefactor.TileComponent();
            component.Type = newType;
            ReplaceComponent(ComponentIds.Tile, component);
            if (previousComponent != null) {
                _tileComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveTile() {
            var component = tile;
            RemoveComponent(ComponentIds.Tile);
            _tileComponentPool.Push(component);
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
