using System.Collections.Generic;
using System.Linq;
using Assets.LevelEditor.Placeables;
using Entitas;

namespace Assets.LevelEditor
{
    public class SelectPlaceableSystem : IInitializeSystem, IExecuteSystem, ISetPool
    {
        private readonly Dictionary<int, IPlaceable> _numberToPlaceable = new Dictionary<int, IPlaceable>
                {
                    { 1, new Tile(MainTileType.Normal.ToString()) },
                    { 2, new Tile(MainTileType.Pillar.ToString(), e => e.IsBlockingTile(true)) },
                    { 3, new Tile(MainTileType.Wall.ToString(), e => e.IsBlockingTile(true)) },
                    { 4, new Tile(MainTileType.SpikeTrap.ToString(), e => e.IsDynamic(true).AddSpikeTrap(false)) },
                    { 5, new Tile(MainTileType.CurseTrigger.ToString(), e => e.IsDynamic(true).IsCurseSwitch(true)) },
                    { 6, new Item(ItemType.Hero.ToString(), e => e.IsDynamic(true).IsHero(true).AddHealth(3)) },
                    { 7, new Item(ItemType.Boss.ToString(), e => e.IsDynamic(true).IsBlockingTile(true).IsBoss(true).IsCursed(true).AddHealth(3)) },
                    { 8, new Spikes() },
                    { 9, new Item(ItemType.Box.ToString(), e => e.IsBlockingTile(true).IsDynamic(true).IsBox(true)) },
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
