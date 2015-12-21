using System;
using System.Linq;
using Entitas;
using UnityEngine;

namespace Assets.EntitasRefactor
{
    public class PlaceTileSystem : IExecuteSystem, ISetPool
    {
        private Pool _pool;
        private Group _inputGroup;
        private Group _tileGroup;

        public void SetPool(Pool pool)
        {
            _pool = pool;
            _inputGroup = pool.GetGroup(Matcher.Input);
            _tileGroup = pool.GetGroup(Matcher.Tile);
        }

        public void Execute()
        {
            var input = _inputGroup.GetSingleEntity();
            if (!input.hasTileSelect || !input.hasPosition)
            {
                return;
            }

            var selectedTile = input.tileSelect.Type;
            var tilePosition = input.position.Value;

            Action<TilePos> clickAction = null;
            if (Input.GetMouseButtonDown(0))
            {
                clickAction = x => AddTile(selectedTile, x);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                clickAction = RemoveTile;
            }

            if (clickAction != null)
            {
                clickAction(tilePosition);
            }
        }

        private void RemoveTile(TilePos tilePos)
        {
            var entities = _tileGroup.GetEntities();
            var selectedTile = entities.SingleOrDefault(x => !x.isPreview && x.hasPosition && x.position.Value == tilePos);
            if(selectedTile != null)
            {
                selectedTile.IsDestroyed(true);
            }
        }

        private void AddTile(MainTileType type, TilePos position)
        {
            _pool.CreateEntity()
                .AddTile(type)
                .AddPosition(position)
                .AddResource("Tiles/Normal");
        }
    }
}
