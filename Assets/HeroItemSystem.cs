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
            var surroundingBoxes = _pool.GetSurroundingEntities(hero.position.Value, Matcher.Box).ToList();

            if (hero.isSpikesCarried && isTrapEmpty)
            {
                PutSpikesInTrap(spikeTrapBelow, hero);
            }
            else if (surroundingBoxes.Any())
            {
                TryPullBox(surroundingBoxes, hero);
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

        private void TryPullBox(List<Entity> surroundingBoxes, Entity hero)
        {
            var box = surroundingBoxes.First();
            var pullDirection = hero.position.Value - box.position.Value;
            var newPlayerPosition = hero.position.Value + pullDirection;
            if (_pool.OpenTileAt(newPlayerPosition))
            {
                box.ReplaceKnocked(pullDirection, true);
                hero.ReplacePosition(newPlayerPosition);
            }
        }

        private static void TakeSpikesFromFloor(Entity spikesOnFloor, Entity hero)
        {
            spikesOnFloor.IsDestroyed(true);
            hero.IsSpikesCarried(true);
        }
    }
}
