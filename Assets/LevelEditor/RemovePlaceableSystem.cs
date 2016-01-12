using Entitas;

namespace Assets.LevelEditor
{
    public class RemovePlaceableSystem : IExecuteSystem, ISetPool
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
            var selectedItem = _pool.GetItemAt(tilePos);
            if (selectedItem != null)
            {
                selectedItem.IsDestroyed(true);
                return;
            }

            var selectedTile = _pool.GetTileAt(tilePos);
            if (selectedTile != null)
            {
                selectedTile.IsDestroyed(true);
            }
        }
    }
}