using Entitas;
using UnityEngine;

namespace Assets.EntitasRefactor.Placeables
{
    public class Tile : IPlaceable
    {
        private readonly MainTileType _type;

        public string Maintype { get { return _type.ToString(); } }

        public Tile(MainTileType type)
        {
            _type = type;
        }

        public void Place(Pool pool, TilePos position)
        {
            var currentTile = pool.GetTileAt(position);

            if(currentTile == null)
            {
                pool.CreateEntity()
                    .AddTile(_type)
                    .AddMaintype(_type.ToString())
                    .AddPosition(position)
                    .AddRotation(Random.Range(0, 4));
            }
            else
            {
                currentTile
                    .ReplaceTile(_type)
                    .ReplaceMaintype(_type.ToString())
                    .ReplacePosition(position)
                    .ReplaceRotation(currentTile.rotation.GetNextRotation());

                if (currentTile.hasSubtype)
                {
                    currentTile.RemoveSubtype();
                }
            }
        }
    }
}