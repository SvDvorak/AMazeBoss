using System.Collections.Generic;
using Entitas;

namespace Assets
{
    public class SpikeTrapSystem : IReactiveSystem, ISetPool
    {
        private Group _bossGroup;
        public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.SpikeTrap, Matcher.ActiveTurn).OnEntityAdded(); } }

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
            if (boss.position.Value == trap.position.Value && trap.spikeTrap.IsLoaded)
            {
                boss.ReplaceHealth(boss.health.Value - 1);
                trap.IsTrapActivated(true);
            }
        }
    }
}
