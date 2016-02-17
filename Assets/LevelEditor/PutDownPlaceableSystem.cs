using System.Collections.Generic;
using Entitas;

namespace Assets.LevelEditor
{
    public class PutDownPlaceableSystem : IReactiveSystem, ISetPool, IEnsureComponents
    {
        public TriggerOnEvent trigger { get { return Matcher.AllOf(GameMatcher.InputPlace, GameMatcher.Position).OnEntityAdded(); } }
        public IMatcher ensureComponents { get { return GameMatcher.Input; } }

        private Pool _pool;

        public void SetPool(Pool pool)
        {
            _pool = pool;
        }

        public void Execute(List<Entity> entities)
        {
            var input = entities.SingleEntity();

            var selectedPlaceable = input.selectedPlaceable.Value;
            var tilePosition = input.position.Value;

            selectedPlaceable.Place(_pool, tilePosition);
        }
    }
}
