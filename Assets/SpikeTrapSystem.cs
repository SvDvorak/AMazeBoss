using System.Collections.Generic;
using System.Linq;
using Entitas;

namespace Assets
{
    public class SpikeTrapSystem : IReactiveSystem, ISetPool, IEnsureComponents
    {
        private Group _characters;
        public TriggerOnEvent trigger { get { return Matcher.ActiveTurn.OnEntityAdded(); } }
        public IMatcher ensureComponents { get { return Matcher.SpikeTrap; } }

        public void SetPool(Pool pool)
        {
            _characters = pool.GetGroup(Matcher.AllOf(Matcher.Position, Matcher.Character));
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
                if (!trap.isTrapActivated && character.position.Value == trap.position.Value && trap.spikeTrap.IsLoaded)
                {
                    character.ReplaceHealth(character.health.Value - 1);
                    trap.IsTrapActivated(true);
                }
            }
        }
    }
}
