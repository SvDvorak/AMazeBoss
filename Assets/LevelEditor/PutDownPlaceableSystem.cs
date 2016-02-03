using System.Collections.Generic;
using Entitas;

namespace Assets.LevelEditor
{
    public class PutDownPlaceableSystem : IReactiveSystem, IEnsureComponents
    {
        public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.InputPlace, Matcher.Position).OnEntityAdded(); } }
        public IMatcher ensureComponents { get { return Matcher.Input; } }

        public void Execute(List<Entity> entities)
        {
            var input = entities.SingleEntity();

            var selectedPlaceable = input.placeableSelected.Value;
            var tilePosition = input.position.Value;

            selectedPlaceable.Place(Pools.pool, tilePosition);
        }
    }
}
