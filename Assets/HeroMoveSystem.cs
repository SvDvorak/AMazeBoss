using System.Collections.Generic;
using System.Linq;
using Entitas;

namespace Assets
{
    public class HeroMoveSystem : IReactiveSystem, ISetPool, IEnsureComponents
    {
        private Pool _pool;
        public TriggerOnEvent trigger { get { return Matcher.AllOf(GameMatcher.Hero, GameMatcher.InputMove).OnEntityAdded(); } }
        public IMatcher ensureComponents { get { return GameMatcher.ActiveTurn; } }

        public void SetPool(Pool pool)
        {
            _pool = pool;
        }

        public void Execute(List<Entity> entities)
        {
            var hero = entities.SingleEntity();
            var moveDirection = hero.inputMove.Direction;
            var newPosition = moveDirection + hero.position.Value;

            var canMoveTo = _pool.OpenTileAt(newPosition);
            var stillInsideSamePuzzle = _pool.IsStillInsideSamePuzzle(hero.position.Value, newPosition);
            var pushableItem = _pool.PushableItemAt(newPosition, moveDirection);

            if (canMoveTo && !(hero.isSpikesCarried && !stillInsideSamePuzzle))
            {
                hero.ReplacePosition(newPosition);
            }
            else if (pushableItem != null)
            {
                pushableItem.ReplaceKnocked(moveDirection, true);
                hero.ReplacePosition(newPosition);
            }
        }
    }
}