using System.Collections.Generic;
using UnityEngine;

namespace Assets.LevelEditor
{
    public class SelectItem : MonoBehaviour
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

        public void Update()
        {
            MainTileType? selected = null;
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