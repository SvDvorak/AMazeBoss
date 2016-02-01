using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

namespace Assets
{
    public class HeroItemSystem : IExecuteSystem, ISetPool
    {
        private Group _heroGroup;
        private Pool _pool;

        public void SetPool(Pool pool)
        {
            _pool = pool;
            _heroGroup = pool.GetGroup(Matcher.Hero);
        }

        public void Execute()
        {
            var hero = _heroGroup.GetSingleEntity();

            if (hero.isCursed)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
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
            else if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                _pool.SwitchCurse();
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
            box.ReplaceKnocked(pullDirection, true);
            hero.ReplacePosition(hero.position.Value + pullDirection);
        }

        private static void TakeSpikesFromFloor(Entity spikesOnFloor, Entity hero)
        {
            spikesOnFloor.IsDestroyed(true);
            hero.IsSpikesCarried(true);
        }
    }
}
