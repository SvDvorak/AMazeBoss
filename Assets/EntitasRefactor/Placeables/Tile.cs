using System;
using Entitas;

namespace Assets.EntitasRefactor.Placeables
{
    public class Tile : Placeable
    {
        public Tile(string type) : base(type)
        {
        }

        public Tile(string type, Action<Entity> addComponentsAction) : base(type, addComponentsAction)
        {
        }

        public override Entity GetExistingEntityAt(Pool pool, TilePos position)
        {
            return pool.GetTileAt(position);
        }
    }
}