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

    public class Spikes : Item
    {
        public Spikes() : base(ItemType.Spikes.ToString(), e => e.IsSpikes(true))
        {
        }

        public override void Place(Pool pool, TilePos position)
        {
            var tileBelow = pool.GetTileAt(position);
            var tileIsSpikeTrap = tileBelow != null && tileBelow.isSpikeTrap;

            if (tileIsSpikeTrap && !tileBelow.hasLoaded)
            {
                tileBelow.AddLoaded(true);
            }
            else
            {
                base.Place(pool, position);
            }
        }
    }
}