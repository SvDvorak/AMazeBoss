using System.Collections.Generic;
using Assets.Level;
using Entitas;

namespace Assets.Items
{
    public class HeroItemSystem : IReactiveSystem, ISetPool, IEnsureComponents
    {
        private Pool _pool;

        public TriggerOnEvent trigger { get { return Matcher.AllOf(GameMatcher.Hero, GameMatcher.InputItemInteract).OnEntityAdded(); } }
        public IMatcher ensureComponents { get { return GameMatcher.ActiveTurn; } }

        public void SetPool(Pool pool)
        {
            _pool = pool;
        }

        public void Execute(List<Entity> entities)
        {
            var hero = entities.SingleEntity();

            var spikesOnFloor = _pool.GetEntityAt(hero.position.Value, x => x.isSpikes);
            var spikeTrapBelow = _pool.GetEntityAt(hero.position.Value, x => x.isSpikeTrap);
            var isEmptyTrapBelow = spikeTrapBelow != null && !spikeTrapBelow.hasLoaded;

            if (hero.isSpikesCarried)
            {
                if (isEmptyTrapBelow)
                {
                    PutSpikesInTrap(spikeTrapBelow, hero);
                }
                else
                {
                    PutSpikesOnFloor(hero);
                }
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

        private void PutSpikesOnFloor(Entity hero)
        {
            WorldObjects.Spikes.Do(_pool.CreateEntity(), _pool).ReplacePosition(hero.position.Value);
            hero.IsSpikesCarried(false);
        }

        private static void TakeSpikesFromFloor(Entity spikesOnFloor, Entity hero)
        {
            spikesOnFloor.IsDestroyed(true);
            hero.IsSpikesCarried(true);
        }
    }
}