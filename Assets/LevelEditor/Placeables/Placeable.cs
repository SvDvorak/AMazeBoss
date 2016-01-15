using System;
using Entitas;
using Random = UnityEngine.Random;

namespace Assets.LevelEditor.Placeables
{
    public interface IPlaceable
    {
        void Place(Pool pool, TilePos position);
        string Maintype { get; }
    }

    public abstract class Placeable : IPlaceable
    {
        private readonly Action<Entity> _addComponentsAction;

        public string Maintype { get; private set; }

        public abstract Entity GetExistingEntityAt(Pool pool, TilePos position);

        protected Placeable(string type, Action<Entity> addComponentsAction)
        {
            Maintype = type;
            _addComponentsAction = addComponentsAction;
        }

        public virtual void Place(Pool pool, TilePos position)
        {
            var currentObject = GetExistingEntityAt(pool, position);
            var newRotation = Random.Range(0, 4);

            if (currentObject != null)
            {
                newRotation = currentObject.rotation.GetNextRotation();
                currentObject.IsDestroyed(true);
            }

            var newObject = pool.CreateEntity()
                .ReplaceMaintype(Maintype)
                .ReplacePosition(position)
                .AddRotation(newRotation);

            _addComponentsAction(newObject);
        }
    }
}