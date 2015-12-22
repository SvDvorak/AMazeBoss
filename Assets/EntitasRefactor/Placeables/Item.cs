using Entitas;
using UnityEngine;

namespace Assets.EntitasRefactor.Placeables
{
    public class Item : IPlaceable
    {
        private readonly ItemType _type;

        public string Maintype { get { return _type.ToString(); } }

        public Item(ItemType type)
        {
            _type = type;
        }

        public void Place(Pool pool, TilePos position)
        {
            var currentItem = pool.GetItemAt(position);

            if (currentItem == null)
            {
                pool.CreateEntity()
                    .AddItem(_type)
                    .AddMaintype(_type.ToString())
                    .AddPosition(position)
                    .AddRotation(Random.Range(0, 4));
            }
            else
            {
                currentItem
                    .ReplaceItem(_type)
                    .ReplaceMaintype(_type.ToString())
                    .ReplaceRotation(currentItem.rotation.GetNextRotation());
            }
        }
    }
}