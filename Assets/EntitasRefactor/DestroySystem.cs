using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Assets.EntitasRefactor
{
    public class DestroySystem : IReactiveSystem, ISetPool
    {
        private Pool _pool;

        public TriggerOnEvent trigger { get { return Matcher.Destroyed.OnEntityAdded(); } }

        public void SetPool(Pool pool)
        {
            _pool = pool;
        }

        public void Execute(List<Entity> entities)
        {
            foreach (var entity in entities)
            {
                if (entity.hasView)
                {
                    GameObject.Destroy(entity.view.Value);
                }
                _pool.DestroyEntity(entity);
            }
        }
    }
}
