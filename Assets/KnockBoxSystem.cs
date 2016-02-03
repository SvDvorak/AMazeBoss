using System.Collections.Generic;
using System.Linq;
using Entitas;

namespace Assets
{
    public class KnockBoxSystem : IReactiveSystem, ISetPool
    {
        private Pool _pool;

        public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.Box, Matcher.Knocked).OnEntityAdded(); } }

        public void SetPool(Pool pool)
        {
            _pool = pool;
        }

        public void Execute(List<Entity> entities)
        {
            foreach (var box in entities)
            {
                var pushedToPosition = box.position.Value + box.knocked.FromDirection;
                var entitiesInFront = _pool.GetEntitiesAt(pushedToPosition).ToList();
                var blockingAtNewPosition = !entitiesInFront.Any() || entitiesInFront.Any(x => x.isBlockingTile || x.isHero);
                if (!blockingAtNewPosition)
                {
                    box.ReplacePosition(pushedToPosition);
                }
            }
        }
    }
}
