using System.Collections.Generic;
using System.Linq;
using Assets.EntitasRefactor.Placeables;
using Entitas;

namespace Assets.EntitasRefactor.Input
{
    public class SelectPlaceableSystem : IInitializeSystem, IExecuteSystem, ISetPool
    {
        private readonly Dictionary<int, Placeable> _numberToPlaceable = new Dictionary<int, Placeable>
                {
                    { 1, new Tile(MainTileType.Normal.ToString(), e => e.IsWalkable(true)) },
                    { 2, new Tile(MainTileType.Pillar.ToString()) },
                    { 3, new Tile(MainTileType.Wall.ToString()) },
                    { 4, new Tile(MainTileType.Spike.ToString(), e => e.IsWalkable(true)) },
                    { 5, new Item(ItemType.Hero.ToString(), e => e.IsHero(true)) },
                    { 6, new Item(ItemType.Boss.ToString(), e => e.IsBoss(true)) }
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

            Placeable selected = null;
            for (int i = 1; i <= _numberToPlaceable.Keys.Count; i++)
            {
                if (UnityEngine.Input.GetKeyDown(i.ToString()))
                {
                    selected = _numberToPlaceable[i];
                }
            }

            SetObjectSelected(selected);
        }

        private void SetObjectSelected(Placeable selected)
        {
            if (selected != null)
            {
                _pool.inputEntity.ReplacePlaceableSelected(selected);
            }
        }
    }
}
