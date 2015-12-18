using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.LevelEditor
{
    public class SelectItem : MonoBehaviour
    {
        private readonly Dictionary<int, Tile> _numberToTileType = new Dictionary<int, Tile>
                {
                    { 1, new Tile(MainTileType.Normal) },
                    { 2, new Tile(MainTileType.Pillar) },
                    { 3, new Tile(MainTileType.Wall) },
                    { 4, new Tile(MainTileType.Spike) },
                    { 5, new Tile(MainTileType.Hero) },
                    { 6, new Tile(MainTileType.Boss) },
                };

        public void Start()
        {
            PublishSelected(_numberToTileType.First().Value);
        }

        public void Update()
        {
            Tile selected = null;
            for (int i = 1; i <= _numberToTileType.Keys.Count; i++)
            {
                if (Input.GetKeyDown(i.ToString()))
                {
                    selected = _numberToTileType[i];
                }
            }

            PublishSelected(selected);
        }

        private static void PublishSelected(Tile selected)
        {
            if (selected != null)
            {
                Events.instance.Raise(new TileSelected(selected));
            }
        }
    }
}