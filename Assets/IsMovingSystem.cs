using Entitas;

namespace Assets
{
    public class IsMovingSystem : IExecuteSystem, ISetPool
    {
        private Group _positionedViewGroup;

        public void SetPool(Pool pool)
        {
            _positionedViewGroup = pool.GetGroup(Matcher.AllOf(Matcher.View, Matcher.Position));
        }

        public void Execute()
        {
            foreach (var entity in _positionedViewGroup.GetEntities())
            {
                entity.IsMoving(entity.position.Value.ToV3() != entity.view.Value.transform.position);
            }
        }
    }
}
