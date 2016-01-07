using System;
using Entitas;

namespace Assets.LevelEditor.Placeables
{
    public class Item : Placeable
    {
        public Item(string type) : this(type, x => { })
        {
        }

        public Item(string type, Action<Entity> addComponentsAction) : base(type, x => AddItemAndDoComponentAction(x, addComponentsAction))
        {
        }

        private static void AddItemAndDoComponentAction(Entity entity, Action<Entity> addComponentsAction)
        {
            entity.IsItem(true);
            addComponentsAction(entity);
        }

        public override Entity GetExistingEntityAt(Pool pool, TilePos position)
        {
            return pool.GetItemAt(position);
        }
    }
}