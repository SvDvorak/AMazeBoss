using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

namespace Assets.EntitasRefactor.Input
{
    public interface IPlaceable
    {
        void Place(Pool pool, TilePos position);
    }

    public class Tile : IPlaceable
    {
        private readonly MainTileType _type;

        public Tile(MainTileType type)
        {
            _type = type;
        }

        public void Place(Pool pool, TilePos position)
        {
            var currentTile = pool.GetTileAt(position);

            if(currentTile == null)
            {
                pool.CreateEntity()
                    .AddTile(_type)
                    .AddMaintype(_type.ToString())
                    .AddPosition(position)
                    .AddRotation(Random.Range(0, 4));
            }
            else
            {
                currentTile
                    .ReplaceTile(_type)
                    .ReplaceMaintype(_type.ToString())
                    .ReplaceRotation((currentTile.rotation.Value + 1)%4);

                if (currentTile.hasSubtype)
                {
                    currentTile.RemoveSubtype();
                }
            }
        }
    }

    public class Item : IPlaceable
    {
        private readonly ItemType _type;

        public Item(ItemType type)
        {
            _type = type;
        }

        public void Place(Pool pool, TilePos position)
        {
            var currentTile = pool.GetTileAt(position);

            if (currentTile == null)
            {
                pool.CreateEntity()
                    .AddItem(_type)
                    .AddPosition(position)
                    .AddRotation(Random.Range(0, 4));
            }
            else
            {
                currentTile.ReplaceItem(_type);
            }
        }
    }

    public class SelectPlaceableSystem : IInitializeSystem, IExecuteSystem, ISetPool
    {
        private readonly Dictionary<int, IPlaceable> _numberToPlaceable = new Dictionary<int, IPlaceable>
                {
                    { 1, new Tile(MainTileType.Normal) },
                    { 2, new Tile(MainTileType.Pillar) },
                    { 3, new Tile(MainTileType.Wall) },
                    { 4, new Tile(MainTileType.Spike) },
                    { 5, new Item(ItemType.Hero) },
                    { 6, new Item(ItemType.Boss) }
                };

        private Pool _pool;

        public void SetPool(Pool pool)
        {
            _pool = pool;
        }

        public void Initialize()
        {
            SetObjectSelected(_numberToPlaceable.First().Value);
        }

        public void Execute()
        {
            if (_pool.isPaused)
            {
                return;
            }

            IPlaceable selected = null;
            for (int i = 1; i <= _numberToPlaceable.Keys.Count; i++)
            {
                if (UnityEngine.Input.GetKeyDown(i.ToString()))
                {
                    selected = _numberToPlaceable[i];
                }
            }

            SetObjectSelected(selected);
        }

        private void SetObjectSelected(IPlaceable selected)
        {
            if (selected != null)
            {
                _pool.inputEntity.ReplacePlaceableSelected(selected);
            }
        }
    }
}
