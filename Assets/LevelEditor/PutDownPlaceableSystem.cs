using Entitas;

namespace Assets.LevelEditor
{
    public class PutDownPlaceableSystem : IExecuteSystem, ISetPool
    {
        private Pool _pool;

        public void SetPool(Pool pool)
        {
            _pool = pool;
        }

        public void Execute()
        {
            if (_pool.isPaused)
            {
                return;
            }

            var input = _pool.inputEntity;
            if (!input.hasPlaceableSelected || !input.hasPosition)
            {
                return;
            }

            var selectedPlaceable = input.placeableSelected.Value;
            var tilePosition = input.position.Value;

            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                selectedPlaceable.Place(Pools.pool, tilePosition);
            }
        }
    }
}
