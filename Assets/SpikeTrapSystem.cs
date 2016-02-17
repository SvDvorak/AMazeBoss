using System.Collections.Generic;
using System.Linq;
using Entitas;

namespace Assets
{
    public class SpikeTrapSystem : IReactiveSystem, ISetPool, IEnsureComponents
    {
        private Group _characters;
        public TriggerOnEvent trigger { get { return GameMatcher.ActiveTurn.OnEntityAdded(); } }
        public IMatcher ensureComponents { get { return Matcher.AllOf(GameMatcher.SpikeTrap, GameMatcher.Loaded); } }

        public void SetPool(Pool pool)
        {
            _characters = pool.GetGroup(Matcher.AllOf(GameMatcher.Position, GameMatcher.Character));
            _characters.OnEntityUpdated += (g, e, i, nc, pc) => RemoveLoadedThisTurnOnCharacterMove(pool);
        }

        public void Execute(List<Entity> entities)
        {
            foreach (var trap in entities.Where(trap => !trap.loaded.LoadedThisTurn))
            {
                DamageIfOnSamePosition(_characters.GetEntities(), trap);
            }
        }

        private void DamageIfOnSamePosition(Entity[] characters, Entity trap)
        {
            foreach (var character in characters)
            {
                if (!trap.isTrapActivated && character.position.Value == trap.position.Value && trap.hasLoaded)
                {
                    character.ReplaceHealth(character.health.Value - 1);
                    trap.IsTrapActivated(true);
                }
            }
        }

        private void RemoveLoadedThisTurnOnCharacterMove(Pool pool)
        {
            var loadedTraps = pool.GetEntities(Matcher.AllOf(GameMatcher.SpikeTrap, GameMatcher.Loaded)).ToList();
            loadedTraps.ForEach(e => e.ReplaceLoaded(false));
        }
    }
}
