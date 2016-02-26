using System;
using Entitas;

namespace Assets.LevelEditor.Placeables
{
    public class Area : Placeable
    {
        public Area(string type, Action<Entity> addComponentsAction) : base(type, x => AddTriggerAndDoComponentAction(x, addComponentsAction))
        {
        }

        private static void AddTriggerAndDoComponentAction(Entity entity, Action<Entity> addComponentsAction)
        {
            entity
                .IsArea(true)
                .AddEditorOnlyVisual(ViewMode.Area);
            addComponentsAction(entity);
        }

        public override Entity GetExistingEntityAt(Pool pool, TilePos position)
        {
            return pool.GetAreaAt(position);
        }
    }
}