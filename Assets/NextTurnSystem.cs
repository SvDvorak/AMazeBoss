using System.Linq;
using Entitas;

namespace Assets
{
    public class NextTurnSystem : IExecuteSystem, ISetPool
    {
        private Group _dynamicGroup;

        public void SetPool(Pool pool)
        {
            _dynamicGroup = pool.GetGroup(Matcher.Dynamic);
        }

        public void Execute()
        {
            var dynamicEntities = _dynamicGroup.GetEntities();

            var hasActingEntities = dynamicEntities.Any(x => x.hasActingTime);
            foreach (var entity in dynamicEntities)
            {
                entity.isActiveTurn = false;
                entity.isActiveTurn = !hasActingEntities;
            }
        }
    }
}
