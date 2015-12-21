using System.Collections.Generic;
using System.Linq;
using Entitas;

namespace Assets.EntitasRefactor.Input
{
    public class SelectTileSystem : IInitializeSystem, IExecuteSystem, ISetPool
    {
        private readonly Dictionary<int, MainTileType> _numberToTileType = new Dictionary<int, MainTileType>
                {
                    { 1, MainTileType.Normal },
                    { 2, MainTileType.Pillar },
                    { 3, MainTileType.Wall },
                    { 4, MainTileType.Spike },
                    { 5, MainTileType.Hero },
                    { 6, MainTileType.Boss },
                };

        private Pool _pool;

        public void SetPool(Pool pool)
        {
            _pool = pool;
        }

        public void Initialize()
        {
            SetTileSelected(_numberToTileType.First().Value);
        }

        public void Execute()
        {
            if (_pool.isPaused)
            {
                return;
            }

            MainTileType? selected = null;
            for (int i = 1; i <= _numberToTileType.Keys.Count; i++)
            {
                if (UnityEngine.Input.GetKeyDown(i.ToString()))
                {
                    selected = _numberToTileType[i];
                }
            }

            SetTileSelected(selected);
        }

        private void SetTileSelected(MainTileType? selected)
        {
            if (selected.HasValue)
            {
                _pool.inputEntity.ReplaceTileSelect(selected.Value);
            }
        }
    }
}
