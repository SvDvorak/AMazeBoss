using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public Assets.EntitasRefactor.TileTemplates tileTemplates { get { return (Assets.EntitasRefactor.TileTemplates)GetComponent(ComponentIds.TileTemplates); } }

        public bool hasTileTemplates { get { return HasComponent(ComponentIds.TileTemplates); } }

        static readonly Stack<Assets.EntitasRefactor.TileTemplates> _tileTemplatesComponentPool = new Stack<Assets.EntitasRefactor.TileTemplates>();

        public static void ClearTileTemplatesComponentPool() {
            _tileTemplatesComponentPool.Clear();
        }

        public Entity AddTileTemplates(Assets.EntitasRefactor.TemplateNames newValue) {
            var component = _tileTemplatesComponentPool.Count > 0 ? _tileTemplatesComponentPool.Pop() : new Assets.EntitasRefactor.TileTemplates();
            component.Value = newValue;
            return AddComponent(ComponentIds.TileTemplates, component);
        }

        public Entity ReplaceTileTemplates(Assets.EntitasRefactor.TemplateNames newValue) {
            var previousComponent = hasTileTemplates ? tileTemplates : null;
            var component = _tileTemplatesComponentPool.Count > 0 ? _tileTemplatesComponentPool.Pop() : new Assets.EntitasRefactor.TileTemplates();
            component.Value = newValue;
            ReplaceComponent(ComponentIds.TileTemplates, component);
            if (previousComponent != null) {
                _tileTemplatesComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveTileTemplates() {
            var component = tileTemplates;
            RemoveComponent(ComponentIds.TileTemplates);
            _tileTemplatesComponentPool.Push(component);
            return this;
        }
    }

    public partial class Pool {
        public Entity tileTemplatesEntity { get { return GetGroup(Matcher.TileTemplates).GetSingleEntity(); } }

        public Assets.EntitasRefactor.TileTemplates tileTemplates { get { return tileTemplatesEntity.tileTemplates; } }

        public bool hasTileTemplates { get { return tileTemplatesEntity != null; } }

        public Entity SetTileTemplates(Assets.EntitasRefactor.TemplateNames newValue) {
            if (hasTileTemplates) {
                throw new SingleEntityException(Matcher.TileTemplates);
            }
            var entity = CreateEntity();
            entity.AddTileTemplates(newValue);
            return entity;
        }

        public Entity ReplaceTileTemplates(Assets.EntitasRefactor.TemplateNames newValue) {
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

    public partial class Matcher {
        static IMatcher _matcherTileTemplates;

        public static IMatcher TileTemplates {
            get {
                if (_matcherTileTemplates == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.TileTemplates);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherTileTemplates = matcher;
                }

                return _matcherTileTemplates;
            }
        }
    }
}
