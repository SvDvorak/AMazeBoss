using System.Collections.Generic;

using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.TileTemplates tileTemplates { get { return (Assets.TileTemplates)GetComponent(GameComponentIds.TileTemplates); } }

        public bool hasTileTemplates { get { return HasComponent(GameComponentIds.TileTemplates); } }

        static readonly Stack<Assets.TileTemplates> _tileTemplatesComponentPool = new Stack<Assets.TileTemplates>();

        public static void ClearTileTemplatesComponentPool() {
            _tileTemplatesComponentPool.Clear();
        }

        public Entity AddTileTemplates(Assets.TemplateNames newValue) {
            var component = _tileTemplatesComponentPool.Count > 0 ? _tileTemplatesComponentPool.Pop() : new Assets.TileTemplates();
            component.Value = newValue;
            return AddComponent(GameComponentIds.TileTemplates, component);
        }

        public Entity ReplaceTileTemplates(Assets.TemplateNames newValue) {
            var previousComponent = hasTileTemplates ? tileTemplates : null;
            var component = _tileTemplatesComponentPool.Count > 0 ? _tileTemplatesComponentPool.Pop() : new Assets.TileTemplates();
            component.Value = newValue;
            ReplaceComponent(GameComponentIds.TileTemplates, component);
            if (previousComponent != null) {
                _tileTemplatesComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveTileTemplates() {
            var component = tileTemplates;
            RemoveComponent(GameComponentIds.TileTemplates);
            _tileTemplatesComponentPool.Push(component);
            return this;
        }
    }

    public partial class Pool {
        public Entity tileTemplatesEntity { get { return GetGroup(GameMatcher.TileTemplates).GetSingleEntity(); } }

        public Assets.TileTemplates tileTemplates { get { return tileTemplatesEntity.tileTemplates; } }

        public bool hasTileTemplates { get { return tileTemplatesEntity != null; } }

        public Entity SetTileTemplates(Assets.TemplateNames newValue) {
            if (hasTileTemplates) {
                throw new EntitasException("Could not set tileTemplates!\n" + this + " already has an entity with Assets.TileTemplates!",
                    "You should check if the pool already has a tileTemplatesEntity before setting it or use pool.ReplaceTileTemplates().");
            }
            var entity = CreateEntity();
            entity.AddTileTemplates(newValue);
            return entity;
        }

        public Entity ReplaceTileTemplates(Assets.TemplateNames newValue) {
            var entity = tileTemplatesEntity;
            if (entity == null) {
                entity = SetTileTemplates(newValue);
            } else {
                entity.ReplaceTileTemplates(newValue);
            }

            return entity;
        }

        public void RemoveTileTemplates() {
            DestroyEntity(tileTemplatesEntity);
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherTileTemplates;

        public static IMatcher TileTemplates {
            get {
                if (_matcherTileTemplates == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.TileTemplates);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherTileTemplates = matcher;
                }

                return _matcherTileTemplates;
            }
        }
    }
