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
}
