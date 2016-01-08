using System.Collections.Generic;
using System.Linq;
using Assets.LevelEditor.Placeables;
using Entitas;

namespace Assets.LevelEditor
{
    public class SelectPlaceableSystem : IInitializeSystem, IExecuteSystem, ISetPool
    {
        private readonly Dictionary<int, Placeable> _numberToPlaceable = new Dictionary<int, Placeable>
                {
                    { 1, new Tile(MainTileType.Normal.ToString(), e => e.IsWalkable(true)) },
                    { 2, new Tile(MainTileType.Pillar.ToString()) },
                    { 3, new Tile(MainTileType.Wall.ToString()) },
                    { 4, new Tile(MainTileType.SpikeTrap.ToString(), e => e.IsWalkable(true).IsDynamic(true).AddSpikeTrap(false)) },
                    { 5, new Item(ItemType.Hero.ToString(), e => e.IsDynamic(true).IsHero(true)) },
                    { 6, new Item(ItemType.Boss.ToString(), e => e.IsDynamic(true).IsBoss(true).AddHealth(3)) },
                    { 7, new Item(ItemType.Spikes.ToString(), e => e.IsSpikes(true)) },
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
