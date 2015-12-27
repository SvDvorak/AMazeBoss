using Assets.Input;
using Entitas;

namespace Assets.LevelEditor
{
    public class RemovePlaceableSystem : InputSystem, IExecuteSystem
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
            var selectedItem = Pool.GetItemAt(tilePos);
            if (selectedItem != null)
            {
                selectedItem.IsDestroyed(true);
                return;
            }

            var selectedTile = Pool.GetTileAt(tilePos);
            if (selectedTile != null)
            {
                selectedTile.IsDestroyed(true);
            }
        }
    }
}