using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;
using UnityEngine;

namespace Assets
{
    public class HeroPickupSystem : IExecuteSystem, ISetPool
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


            var spikes = _pool.GetEntityAt(hero.position.Value, Matcher.Spikes);

            if (UnityEngine.Input.GetKeyDown(KeyCode.Space) && spikes != null)
            {
                spikes.IsDestroyed(true);
            }
        }
    }
}
