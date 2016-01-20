using System.Collections.Generic;
using System.Linq;
using Entitas;

namespace Assets
{
    public class KnockBoxSystem : IReactiveSystem, ISetPool
    {
        private Group _boxGroup;
        private Pool _pool;
        public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.Boss, Matcher.Position).OnEntityAdded(); } }

        public void SetPool(Pool pool)
        {
            _pool = pool;
            _boxGroup = pool.GetGroup(Matcher.Box);
        }

        public void Execute(List<Entity> entities)
        {
            var boss = entities.SingleEntity();

            if (!boss.isBossSprinting)
            {
                return;
            }

            _boxGroup.GetEntities()
                .Select(e => new { entity = e, posDelta = e.position.Value - boss.position.Value })
                .Where(x => x.posDelta.ManhattanDistance() == 1 && _pool.CanMoveTo(x.entity.position.Value + x.posDelta))
                .ToList()
                .ForEach(x => KnockBox(x.entity, x.posDelta));

        }

        private void KnockBox(Entity entity, TilePos knockDirection)
        {
            entity.ReplacePosition(entity.position.Value + knockDirection);
        }
    }
}
