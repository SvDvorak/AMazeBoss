using System.Collections.Generic;
using Entitas;

namespace Assets
{
    public class HeroPullBoxSystem : IReactiveSystem, ISetPool, IEnsureComponents
    {
        private Pool _pool;

        public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.Hero, Matcher.InputPullItem).OnEntityAdded(); } }
        public IMatcher ensureComponents { get { return Matcher.ActiveTurn; } }

        public void SetPool(Pool pool)
        {
            _pool = pool;
        }

        public void Execute(List<Entity> entities)
        {
            var hero = entities.SingleEntity();
            var pullDirection = hero.inputPullItem.Direction;
            var boxPosition = hero.position.Value - pullDirection;
            var box = _pool.GetEntityAt(boxPosition, Matcher.Box);

            if (box != null)
            {
                var newHeroPosition = hero.position.Value + pullDirection;
                var playerHasOpenSpaceToMoveTo = _pool.OpenTileAt(newHeroPosition);
                if (playerHasOpenSpaceToMoveTo)
                {
                    box.ReplaceKnocked(pullDirection, true);
                    hero.ReplacePosition(newHeroPosition);
                }
            }
        }
    }
}