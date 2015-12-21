using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.LevelEditor
{
    public class SelectItem : MonoBehaviour
    {
        private readonly Dictionary<int, StandardTile> _numberToTileType = new Dictionary<int, StandardTile>
                {
                    { 1, new StandardTile(MainTileType.Normal) },
                    { 2, new StandardTile(MainTileType.Pillar) },
                    { 3, new WallTile() },
                    { 4, new StandardTile(MainTileType.Spike) },
                    { 5, new StandardTile(MainTileType.Hero) },
                    { 6, new StandardTile(MainTileType.Boss) },
                };

        public void Start()
        {
            PublishSelected(_numberToTileType.First().Value);
        }

        public void Update()
        {
            StandardTile selected = null;
            for (int i = 1; i <= _numberToTileType.Keys.Count; i++)
            {
                if (Input.GetKeyDown(i.ToString()))
                {
                    selected = _numberToTileType[i];
                }
            }

            PublishSelected(selected);
        }

        private static void PublishSelected(StandardTile selected)
        {
            if (selected != null)
            {
                Events.instance.Raise(new TileSelected(selected));
            }
        }
    }
}