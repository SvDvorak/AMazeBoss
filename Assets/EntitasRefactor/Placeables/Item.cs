using System;
using Entitas;

namespace Assets.EntitasRefactor.Placeables
{
    public class Item : Placeable
    {
        public Item(string type) : base(type)
        {
        }

        public Item(string type, Action<Entity> addComponentsAction) : base(type, addComponentsAction)
        {
        }

        public override Entity GetExistingEntityAt(Pool pool, TilePos position)
        {
            return pool.GetItemAt(position);
        }
    }
}