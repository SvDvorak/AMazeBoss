using Entitas;

namespace Assets.EntitasRefactor.Input
{
    public class PutDownPlaceableSystem : InputSystem, IExecuteSystem
    {
        public void Execute()
        {
            if (Pool.isPaused)
            {
                return;
            }

            var input = InputGroup.GetSingleEntity();
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
