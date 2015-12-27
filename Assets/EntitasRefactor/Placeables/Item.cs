using System;
using Entitas;
using Random = UnityEngine.Random;

namespace Assets.EntitasRefactor.Placeables
{
    public class Item : IPlaceable
    {
        private readonly ItemType _type;
        private readonly Action<Entity> _addComponentsAction;

        public string Maintype { get { return _type.ToString(); } }

        public Item(ItemType type) : this(type, x => { })
        {
        }

        public Item(ItemType type, Action<Entity> addComponentsAction)
        {
            _type = type;
            _addComponentsAction = addComponentsAction;
        }

        public virtual Entity Place(Pool pool, TilePos position)
        {
            var currentItem = pool.GetItemAt(position);
            var newRotation = Random.Range(0, 4);

            if (currentItem != null)
            {
                newRotation = currentItem.rotation.GetNextRotation();
                currentItem.IsDestroyed(true);
            }

            var newItem = pool.CreateEntity()
                .IsItem(true)
                .ReplaceMaintype(_type.ToString())
                .ReplacePosition(position)
                .AddRotation(newRotation);

            _addComponentsAction(newItem);

            return newItem;
        }
    }
}