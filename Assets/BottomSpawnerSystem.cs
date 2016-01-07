using System.Collections.Generic;
using Entitas;

namespace Assets
{
    public class BottomSpawnerSystem : IReactiveSystem, ISetPool
    {
        private Pool _pool;

        public TriggerOnEvent trigger { get { return Matcher.Tile.OnEntityAdded(); } }

        public void SetPool(Pool pool)
        {
            _pool = pool;
        }

        public void Execute(List<Entity> entities)
        {
            foreach (var entity in entities)
            {
                AddOrUpdateBottomFor(entity);
            }
        }

        private void AddOrUpdateBottomFor(Entity entity)
        {
            Entity child;
            if (entity.hasParent)
            {
                child = _pool.FindChildFor(entity);
            }
            else
            {
                entity.AddParent();
                child = _pool.CreateEntity();
            }

            child
                .ReplaceChild(entity.parent.Id)
                .ReplacePosition(entity.position.Value)
                .ReplaceResource("Bottoms/Empty");
        }
    }
}
