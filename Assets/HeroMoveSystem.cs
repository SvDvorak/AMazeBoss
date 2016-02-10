using System.Collections.Generic;
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
            var canPush = _pool.PushableItemAt(newPosition);

            if (canMoveTo)
            {
                hero.ReplacePosition(newPosition);
            }
            else if (canPush)
            {
                var canPushBox = _pool.OpenTileAt(newPosition + moveDirection);
                if (canPushBox)
                {
                    _pool.KnockObjectsInFront(hero.position.Value, moveDirection, true);
                    hero.ReplacePosition(newPosition);
                }
            }
        }
    }
}