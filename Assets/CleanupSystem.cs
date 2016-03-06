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
            _pool.isLevelLoaded = false;

            _pool
                .GetEntities()
                .ForEach(x =>
                {
                    x.isAttacking = false;
                    x.isPulling = false;
                    x.hasBumpedIntoObject = false;
                    x.isRocked = false;
                    x.isInputItemInteract = false;
                    x.isInputCurseSwitch = false;
                });
        }
    }
}