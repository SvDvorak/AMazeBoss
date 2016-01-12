using System.Collections.Generic;
using System.Linq;
using Entitas;

namespace Assets
{
    public class SpikeTrapSystem : IReactiveSystem, ISetPool
    {
        private Group _bossGroup;
        public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.SpikeTrap, Matcher.ActiveTurn).OnEntityAdded(); } }

        public void SetPool(Pool pool)
        {
            _bossGroup = pool.GetGroup(Matcher.AllOf(Matcher.Boss, Matcher.Position));
            _bossGroup.OnEntityUpdated += (g, e, i, nc, pc) => RemoveActivationOnBossMove(pool, e);
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
            if (!trap.hasSpikedTarget && boss.position.Value == trap.position.Value && trap.spikeTrap.IsLoaded)
            {
                boss.ReplaceHealth(boss.health.Value - 1);
                trap.IsTrapActivated(true);
                trap.AddSpikedTarget(boss.id.Value);
            }
        }

        private void RemoveActivationOnBossMove(Pool pool, Entity boss)
        {
            var activatedTrap = pool.GetEntities(Matcher.AllOf(Matcher.SpikeTrap, Matcher.SpikedTarget));
            foreach(var trap in activatedTrap.Where(e => e.spikedTarget.BossId == boss.id.Value))
            {
                if (trap.position.Value != boss.position.Value)
                {
                    trap.RemoveSpikedTarget();
                }
            }
        }
    }
}
