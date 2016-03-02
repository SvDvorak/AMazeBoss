using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

namespace Assets
{
    public class PositionsCacheUpdateSystem : IInitializeSystem, ISetPool
    {
        private Pool _pool;
        private Group _positionableGroup;

        public void SetPool(Pool pool)
        {
            _pool = pool;
        }

        public void Initialize()
        {
            _positionableGroup = _pool.GetGroup(GameMatcher.Position);
            _positionableGroup.OnEntityAdded += (g, e, i, c) => AddToCache(e, c as PositionComponent);
            _positionableGroup.OnEntityRemoved += (g, e, i, c) => RemoveFromCache(e, c as PositionComponent);
        }

        private Dictionary<TilePos, List<Entity>> GetCache { get { return _pool.objectPositionCache.Cache; } }

        private void AddToCache(Entity entity, PositionComponent positionComponent)
        {
            var cache = GetCache;
            entity.Retain(this);
            if (cache.ContainsKey(positionComponent.Value))
            {
                cache[positionComponent.Value].Add(entity);
            }
            else
            {
                cache[positionComponent.Value] = new List<Entity> { entity };
            }
        }

        private void RemoveFromCache(Entity entity, PositionComponent positionComponent)
        {
            entity.Release(this);
            if (_pool.hasObjectPositionCache)
            {
                var cache = GetCache;
                if (cache.ContainsKey(positionComponent.Value))
                {
                    var positionableAtPosition = cache[positionComponent.Value];
                    positionableAtPosition.Remove(entity);
                    if (!positionableAtPosition.Any())
                    {
                        cache.Remove(positionComponent.Value);
                    }
                }
            }
        }
    }
}
