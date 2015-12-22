using System.Collections.Generic;
using System.Linq;
using Assets.EntitasRefactor.Placeables;
using Entitas;

namespace Assets.EntitasRefactor.Input
{
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
