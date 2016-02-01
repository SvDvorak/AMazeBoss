using System;
using Entitas;

namespace Assets
{
    public class CleanupSystem : IExecuteSystem, ISetPool
    {
        private Pool _pool;

        public void SetPool(Pool pool)
        {
            _pool = pool;
        }

        public void Execute()
        {
            _pool.GetEntities()
                .DoForAll(x => x.isInputItemInteract = false)
                .DoForAll(x => x.isInputCurseSwitch = false);
        }
    }

    public static class CleanupPoolExtensions
    {
        public static Entity[] DoForAll(this Entity[] entities, Action<Entity> action)
        {
            foreach (var entity in entities)
            {
                action(entity);
            }
            return entities;
        } 
    }
}
