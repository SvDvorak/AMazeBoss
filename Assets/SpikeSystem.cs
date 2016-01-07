using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;

namespace Assets
{
    public class SpikeSystem : IReactiveSystem, ISetPool
    {
        private Group _bossGroup;
        public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.Spike, Matcher.ActiveTurn).OnEntityAdded(); } }

        public void SetPool(Pool pool)
        {
            _bossGroup = pool.GetGroup(Matcher.Boss);
        }

        public void Execute(List<Entity> entities)
        {
            var boss = _bossGroup.GetSingleEntity();

            foreach (var trap in entities)
            {
                DamageIfOnSamePosition(boss, trap);
            }
        }

        private void DamageIfOnSamePosition(Entity boss, Entity trap)
        {
            if (boss.position.Value == trap.position.Value)
            {
                boss.ReplaceHealth(boss.health.Value - 1);
                trap.ReplaceActingTime(2);
            }
        }
    }
}
