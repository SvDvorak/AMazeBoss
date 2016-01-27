using System.Collections.Generic;
using System.Linq;
using Entitas;

namespace Assets
{
    public class SpikeTrapSystem : IReactiveSystem, ISetPool
    {
        private Group _characters;
        public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.SpikeTrap, Matcher.ActiveTurn).OnEntityAdded(); } }

        public void SetPool(Pool pool)
        {
            _characters = pool.GetGroup(Matcher.AllOf(Matcher.Position, Matcher.Character));
            _characters.OnEntityUpdated += (g, e, i, nc, pc) => RemoveActivationOnCharacterMove(pool, e);
        }

        public void Execute(List<Entity> entities)
        {
            foreach (var trap in entities)
            {
                DamageIfOnSamePosition(_characters.GetEntities(), trap);
            }
        }

        private void DamageIfOnSamePosition(Entity[] characters, Entity trap)
        {
            foreach (var character in characters)
            {
                if (!trap.hasSpikedTarget && character.position.Value == trap.position.Value && trap.spikeTrap.IsLoaded)
                {
                    character.ReplaceHealth(character.health.Value - 1);
                    trap.IsTrapActivated(true);
                    trap.AddSpikedTarget(character.id.Value);
                }
            }
        }

        private void RemoveActivationOnCharacterMove(Pool pool, Entity character)
        {
            var activatedTrap = pool.GetEntities(Matcher.AllOf(Matcher.SpikeTrap, Matcher.SpikedTarget));
            foreach(var trap in activatedTrap.Where(e => e.spikedTarget.TargetId == character.id.Value))
            {
                if (trap.position.Value != character.position.Value)
                {
                    trap.RemoveSpikedTarget();
                }
            }
        }
    }
}
