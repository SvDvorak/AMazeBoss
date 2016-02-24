using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.ObjectPositionCacheComponent objectPositionCache { get { return (Assets.ObjectPositionCacheComponent)GetComponent(GameComponentIds.ObjectPositionCache); } }

        public bool hasObjectPositionCache { get { return HasComponent(GameComponentIds.ObjectPositionCache); } }

        public Entity AddObjectPositionCache(System.Collections.Generic.Dictionary<Assets.TilePos, System.Collections.Generic.List<Entitas.Entity>> newCache) {
            var componentPool = GetComponentPool(GameComponentIds.ObjectPositionCache);
            var component = (Assets.ObjectPositionCacheComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.ObjectPositionCacheComponent());
            component.Cache = newCache;
            return AddComponent(GameComponentIds.ObjectPositionCache, component);
        }

        public Entity ReplaceObjectPositionCache(System.Collections.Generic.Dictionary<Assets.TilePos, System.Collections.Generic.List<Entitas.Entity>> newCache) {
            var componentPool = GetComponentPool(GameComponentIds.ObjectPositionCache);
            var component = (Assets.ObjectPositionCacheComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.ObjectPositionCacheComponent());
            component.Cache = newCache;
            ReplaceComponent(GameComponentIds.ObjectPositionCache, component);
            return this;
        }

        public Entity RemoveObjectPositionCache() {
            return RemoveComponent(GameComponentIds.ObjectPositionCache);;
        }
    }

    public partial class Pool {
        public Entity objectPositionCacheEntity { get { return GetGroup(GameMatcher.ObjectPositionCache).GetSingleEntity(); } }

        public Assets.ObjectPositionCacheComponent objectPositionCache { get { return objectPositionCacheEntity.objectPositionCache; } }

        public bool hasObjectPositionCache { get { return objectPositionCacheEntity != null; } }

        public Entity SetObjectPositionCache(System.Collections.Generic.Dictionary<Assets.TilePos, System.Collections.Generic.List<Entitas.Entity>> newCache) {
            if (hasObjectPositionCache) {
                throw new EntitasException("Could not set objectPositionCache!\n" + this + " already has an entity with Assets.ObjectPositionCacheComponent!",
                    "You should check if the pool already has a objectPositionCacheEntity before setting it or use pool.ReplaceObjectPositionCache().");
            }
            var entity = CreateEntity();
            entity.AddObjectPositionCache(newCache);
            return entity;
        }

        public Entity ReplaceObjectPositionCache(System.Collections.Generic.Dictionary<Assets.TilePos, System.Collections.Generic.List<Entitas.Entity>> newCache) {
            var entity = objectPositionCacheEntity;
            if (entity == null) {
                entity = SetObjectPositionCache(newCache);
            } else {
                entity.ReplaceObjectPositionCache(newCache);
            }

            return entity;
        }

        public void RemoveObjectPositionCache() {
            DestroyEntity(objectPositionCacheEntity);
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherObjectPositionCache;

        public static IMatcher ObjectPositionCache {
            get {
                if (_matcherObjectPositionCache == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.ObjectPositionCache);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherObjectPositionCache = matcher;
                }

                return _matcherObjectPositionCache;
            }
        }
    }
