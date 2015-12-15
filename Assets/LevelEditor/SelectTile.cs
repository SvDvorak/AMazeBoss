using UnityEngine;

namespace Assets.LevelEditor
{
    public class SelectTile : MonoBehaviour
    {
        public void Update()
        {
            TileType? selected = null;
            for (int i = 1; i <= TileTypeHelper.GetCount(); i++)
            {
                if (Input.GetKeyDown(i.ToString()))
                {
                    selected = (TileType?) i - 1;
                }
            }

            if (selected.HasValue)
            {
                Events.instance.Raise(new TileSelected(selected.Value));
            }
        }
    }
}
