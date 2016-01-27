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

                if (hero.isSpikesCarried)
                {
                    if (spikeTrapBelow != null && !spikeTrapBelow.spikeTrap.IsLoaded)
                    {
                        PutSpikesInTrap(spikeTrapBelow, hero);
                    }
                }
                else
                {
                    if (spikesOnFloor != null)
                    {
                        TakeSpikesFromFloor(spikesOnFloor, hero);
                    }
                    else if (spikeTrapBelow != null && spikeTrapBelow.spikeTrap.IsLoaded)
                    {
                        TakeSpikesFromTrap(spikeTrapBelow, hero);
                    }
                }
            }
            else if (UnityEngine.Input.GetKeyDown(KeyCode.LeftControl))
            {
                _pool.SwitchCurse();
            }
        }

        private static void PutSpikesInTrap(Entity spikeTrapBelow, Entity hero)
        {
            spikeTrapBelow.ReplaceSpikeTrap(true);
            hero.IsSpikesCarried(false);
        }

        private static void TakeSpikesFromFloor(Entity spikesOnFloor, Entity hero)
        {
            spikesOnFloor.IsDestroyed(true);
            hero.IsSpikesCarried(true);
        }

        private static void TakeSpikesFromTrap(Entity spikeTrapBelow, Entity hero)
        {
            spikeTrapBelow.ReplaceSpikeTrap(false);
            hero.IsSpikesCarried(true);
        }
    }
}
