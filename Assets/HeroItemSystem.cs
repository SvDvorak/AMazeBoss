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

            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
            {
                var spikesOnFloor = _pool.GetEntityAt(hero.position.Value, Matcher.Spikes);
                var spikeTrapBelow = _pool.GetEntityAt(hero.position.Value, Matcher.SpikeTrap);
                if (!hero.isSpikesCarried && spikesOnFloor != null)
                {
                    spikesOnFloor.IsDestroyed(true);
                    hero.IsSpikesCarried(true);
                }
                else if (hero.isSpikesCarried && spikeTrapBelow != null)
                {
                    spikeTrapBelow.ReplaceSpikeTrap(true);
                    hero.IsSpikesCarried(false);
                }
            }
            else if (UnityEngine.Input.GetKeyDown(KeyCode.LeftControl))
            {
                _pool.SwitchCurse();
            }
        }
    }
}
