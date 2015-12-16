using System.Collections.Generic;
using UnityEngine;

namespace Assets.LevelEditor
{
    public class SelectTile : MonoBehaviour
    {
        private Dictionary<int, TileType> _numberToTileType;

        public void Start()
        {
            _numberToTileType = new Dictionary<int, TileType>
                {
                    { 1, TileType.Normal },
                    { 2, TileType.Pillar },
                    { 3, TileType.Wall },
                    { 4, TileType.Spike }
                };
        }

        public void Update()
        {
            TileType? selected = null;
            for (int i = 1; i <= _numberToTileType.Keys.Count; i++)
            {
                if (Input.GetKeyDown(i.ToString()))
                {
                    selected = _numberToTileType[i];
                }
            }

            if (selected.HasValue)
            {
                Events.instance.Raise(new TileSelected(selected.Value));
            }
        }
    }
}