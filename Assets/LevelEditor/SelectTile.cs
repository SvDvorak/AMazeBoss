using System;
using UnityEngine;

namespace Assets
{
    public class SelectTile : MonoBehaviour
    {
        public void Update()
        {
            TileType? Selected = null;
            for (int i = 1; i <= TileTypeHelper.GetCount(); i++)
            {
                if (Input.GetKeyDown(i.ToString()))
                {
                    Selected = (TileType?) i - 1;
                }
            }

            if (Selected.HasValue)
            {
                Events.instance.Raise(new TileSelected(Selected.Value));
            }
        }
    }
}
