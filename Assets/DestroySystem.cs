using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

namespace Assets
{
    public class DestroySystem : IReactiveSystem, ISetPool
    {
        private Pool _pool;

        public TriggerOnEvent trigger { get { return GameMatcher.Destroyed.OnEntityAdded(); } }

        public void SetPool(Pool pool)
        {
            _pool = pool;
        }

        public void Execute(List<Entity> entities)
        {
            foreach (var entity in entities)
            {
                var hasBeenDestroyedAsChild = !entity.isDestroyed;
                if (hasBeenDestroyedAsChild)
                {
                    continue;
                }

                if (entity.hasView)
                {
                    GameObject.Destroy(entity.view.Value);
                    GameObjectConfigurer.DetachEntity(entity.view.Value, entity);
                }
                if (entity.hasId)
                {
                    Execute(_pool.FindChildrenFor(entity));
                }

                _pool.DestroyEntity(entity);
            }
        }
    }
}
