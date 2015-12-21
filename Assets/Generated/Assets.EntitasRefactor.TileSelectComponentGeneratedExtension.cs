using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public Assets.EntitasRefactor.TileSelectComponent tileSelect { get { return (Assets.EntitasRefactor.TileSelectComponent)GetComponent(ComponentIds.TileSelect); } }

        public bool hasTileSelect { get { return HasComponent(ComponentIds.TileSelect); } }

        static readonly Stack<Assets.EntitasRefactor.TileSelectComponent> _tileSelectComponentPool = new Stack<Assets.EntitasRefactor.TileSelectComponent>();

        public static void ClearTileSelectComponentPool() {
            _tileSelectComponentPool.Clear();
        }

        public Entity AddTileSelect(Assets.MainTileType newType) {
            var component = _tileSelectComponentPool.Count > 0 ? _tileSelectComponentPool.Pop() : new Assets.EntitasRefactor.TileSelectComponent();
            component.Type = newType;
            return AddComponent(ComponentIds.TileSelect, component);
        }

        public Entity ReplaceTileSelect(Assets.MainTileType newType) {
            var previousComponent = hasTileSelect ? tileSelect : null;
            var component = _tileSelectComponentPool.Count > 0 ? _tileSelectComponentPool.Pop() : new Assets.EntitasRefactor.TileSelectComponent();
            component.Type = newType;
            ReplaceComponent(ComponentIds.TileSelect, component);
            if (previousComponent != null) {
                _tileSelectComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveTileSelect() {
            var component = tileSelect;
            RemoveComponent(ComponentIds.TileSelect);
            _tileSelectComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherTileSelect;

        public static IMatcher TileSelect {
            get {
                if (_matcherTileSelect == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.TileSelect);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherTileSelect = matcher;
                }

                return _matcherTileSelect;
            }
        }
    }
}
