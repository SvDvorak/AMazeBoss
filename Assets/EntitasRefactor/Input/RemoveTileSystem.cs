using Entitas;

namespace Assets.EntitasRefactor.Input
{
    public class RemoveTileSystem : InputSystem, IExecuteSystem
    {
        public void Execute()
        {
            if (Pool.isPaused)
            {
                return;
            }

            var input = InputGroup.GetSingleEntity();
            if (!input.hasPosition)
            {
                return;
            }

            var tilePosition = input.position.Value;

            if (UnityEngine.Input.GetMouseButtonDown(1))
            {
                RemoveTile(tilePosition);
            }
        }

        private void RemoveTile(TilePos tilePos)
        {
            var selectedTile = Pool.GetTileAt(tilePos);
            if (selectedTile != null)
            {
                selectedTile.IsDestroyed(true);
            }
        }
    }
}