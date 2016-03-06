using System.Collections.Generic;
using Entitas;

namespace Assets
{
    public class HeroPullBoxSystem : IReactiveSystem, ISetPool, IEnsureComponents
    {
        private Pool _pool;

        public TriggerOnEvent trigger { get { return Matcher.AllOf(GameMatcher.Hero, GameMatcher.InputPullItem).OnEntityAdded(); } }
        public IMatcher ensureComponents { get { return GameMatcher.ActiveTurn; } }

        public void SetPool(Pool pool)
        {
            _pool = pool;
        }

        public void Execute(List<Entity> entities)
        {
            var hero = entities.SingleEntity();
            var pullDirection = hero.inputPullItem.Direction;
            var pushablePosition = hero.position.Value - pullDirection;
            var pushableItem = _pool.PushableItemAt(pushablePosition, pullDirection);

            if (pushableItem != null)
            {
                var newHeroPosition = hero.position.Value + pullDirection;
                var playerHasOpenSpaceToMoveTo = _pool.OpenTileAt(newHeroPosition);
                if (playerHasOpenSpaceToMoveTo)
                {
                    pushableItem.ReplaceKnocked(pullDirection, false, 0);
                    hero.ReplacePosition(newHeroPosition);
                    hero.ReplaceRotation(LocalDirections.ToRotation(-pullDirection));
                    hero.IsPulling(true);
                }
            }
        }
    }
}