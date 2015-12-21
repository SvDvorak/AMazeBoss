using Entitas;
using UnityEngine;

namespace Assets.EntitasRefactor.Input
{
    public class PlaceTileSystem : InputSystem, IExecuteSystem
    {
        public void Execute()
        {
            if (Pool.isPaused)
            {
                return;
            }

            var input = InputGroup.GetSingleEntity();
            if (!input.hasTileSelect || !input.hasPosition)
            {
                return;
            }

            var selectedType = input.tileSelect.Type;
            var tilePosition = input.position.Value;

            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                var currentTile = Pool.GetTileAt(tilePosition);
                if (currentTile == null)
                {
                    AddTile(selectedType, tilePosition);
                }
                else
                {
                    UpdateTile(currentTile, selectedType);
                }
            }
        }

        private void AddTile(MainTileType type, TilePos position)
        {
            Pool.CreateEntity()
                .AddTile(type)
                .AddPosition(position)
                .AddRotation(Random.Range(0, 4));
        }

        private void UpdateTile(Entity tile, MainTileType selectedType)
        {
            tile.ReplaceTile(selectedType);

            if (tile.hasSubtype)
            {
                tile.RemoveSubtype();
            }
        }
    }
}
