using System.Linq;
using Entitas;

namespace Assets
{
    public class NextTurnSystem : IExecuteSystem, ISetPool
    {
        private Group _dynamicGroup;

        public void SetPool(Pool pool)
        {
            _dynamicGroup = pool.GetGroup(GameMatcher.Dynamic);
        }

        public void Execute()
        {
            var dynamicEntities = _dynamicGroup.GetEntities();

            var hasActingEntities = dynamicEntities.Any(x => x.hasActingTime);
            if(!hasActingEntities)
            {
                foreach (var e in dynamicEntities.Where(x => !x.hasActingTime && x.hasQueueActing))
                {
                    e.queueActing.Action();
                    e.AddActingTime(e.queueActing.Time);
                    e.RemoveQueueActing();
                }
            }

            hasActingEntities = dynamicEntities.Any(x => x.hasActingTime || x.hasActingActions);
            foreach (var entity in dynamicEntities)
            {
                entity.isActiveTurn = false;
                entity.isActiveTurn = !hasActingEntities;
            }
        }
    }
}
