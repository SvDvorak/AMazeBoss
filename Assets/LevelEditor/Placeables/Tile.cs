using System;
using Entitas;

namespace Assets.LevelEditor.Placeables
{
    public class Tile : Placeable
    {
        public Tile(string type) : this(type, x => { })
        {
        }

        public Tile(string type, Action<Entity> addComponentsAction) : base(type, x => AddTileAndDoComponentAction(x, addComponentsAction))
        {
        }

        private static void AddTileAndDoComponentAction(Entity entity, Action<Entity> addComponentsAction)
        {
            entity.IsTile(true);
            addComponentsAction(entity);
        }

        public override Entity GetExistingEntityAt(Pool pool, TilePos position)
        {
            return pool.GetTileAt(position);
        }
    }
}