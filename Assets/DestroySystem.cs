using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

namespace Assets
{
    public class DestroySystem : IReactiveSystem, ISetPool
    {
        private Pool _pool;

        public TriggerOnEvent trigger
        {
            get { return GameMatcher.Destroyed.OnEntityAdded(); }
        }

        public void SetPool(Pool pool)
        {
            _pool = pool;
        }

        public void Execute(List<Entity> entities)
        {
            var toDeleteWithChildren = entities
                .Concat(entities
                    .Where(x => x.hasId)
                    .SelectMany(x => _pool.FindChildrenFor(x)))
                .Distinct()
                .ToList();

            foreach (var entity in toDeleteWithChildren)
            {
                if (entity.hasView)
                {
                    Object.Destroy(entity.view.Value);
                    GameObjectConfigurer.DetachEntity(entity.view.Value, entity);
                }

                _pool.DestroyEntity(entity);
            }
        }
    }
}