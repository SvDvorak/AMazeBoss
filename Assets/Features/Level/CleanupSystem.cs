using Entitas;

namespace Assets.Level
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
                    x.isPushing = false;
                    x.hasBumpedIntoObject = false;
                    x.hasRecoveredAtEdge = false;
                    x.isRocked = false;
                    x.isInputItemInteract = false;
                    x.isInputCurseSwitch = false;
                });
        }
    }
}