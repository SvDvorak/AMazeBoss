using System.Collections.Generic;
using System.Linq;
using Entitas;

namespace Assets
{
    public class HeroItemSystem : IReactiveSystem, ISetPool, IEnsureComponents
    {
        private Pool _pool;

        public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.Hero, Matcher.InputItemInteract).OnEntityAdded(); } }
        public IMatcher ensureComponents { get { return Matcher.ActiveTurn; } }

        public void SetPool(Pool pool)
        {
            _pool = pool;
        }

        public void Execute(List<Entity> entities)
        {
            var hero = entities.SingleEntity();

            var spikesOnFloor = _pool.GetEntityAt(hero.position.Value, Matcher.Spikes);
            var spikeTrapBelow = _pool.GetEntityAt(hero.position.Value, Matcher.SpikeTrap);
            var isTrapEmpty = spikeTrapBelow != null && !spikeTrapBelow.hasLoaded;

            if (hero.isSpikesCarried && isTrapEmpty)
            {
                PutSpikesInTrap(spikeTrapBelow, hero);
            }
            else if (!hero.isSpikesCarried && spikesOnFloor != null)
            {
                TakeSpikesFromFloor(spikesOnFloor, hero);
            }
        }

        private static void PutSpikesInTrap(Entity spikeTrap, Entity hero)
        {
            spikeTrap.AddLoaded(true);
            hero.IsSpikesCarried(false);
        }

        private static void TakeSpikesFromFloor(Entity spikesOnFloor, Entity hero)
        {
            spikesOnFloor.IsDestroyed(true);
            hero.IsSpikesCarried(true);
        }
    }
}
