using System;
using Entitas;
using Random = UnityEngine.Random;

namespace Assets.EntitasRefactor.Placeables
{
    public class Tile : IPlaceable
    {
        private readonly MainTileType _type;
        private readonly Action<Entity> _addComponentsAction;

        public string Maintype { get { return _type.ToString(); } }

        public Tile(MainTileType type) : this(type, x => { })
        {
        }

        public Tile(MainTileType type, Action<Entity> addComponentsAction)
        {
            _type = type;
            _addComponentsAction = addComponentsAction;
        }

        public Entity Place(Pool pool, TilePos position)
        {
            var currentTile = pool.GetTileAt(position);
            var newRotation = Random.Range(0, 4);

            if (currentTile != null)
            {
                newRotation = currentTile.rotation.GetNextRotation();
                currentTile.IsDestroyed(true);
            }

            var newTile = pool.CreateEntity()
                .IsTile(true)
                .ReplaceMaintype(_type.ToString())
                .ReplacePosition(position)
                .AddRotation(newRotation);

            _addComponentsAction(newTile);

            return newTile;
        }
    }
}