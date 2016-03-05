using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

namespace Assets
{
    public class BottomSpawnerSystem : IReactiveSystem, ISetPool, IExcludeComponents
    {
        private Pool _pool;

        public TriggerOnEvent trigger { get { return GameMatcher.GameObject.OnEntityAdded(); } }
        public IMatcher excludeComponents { get { return GameMatcher.Preview; } }

        public void SetPool(Pool pool)
        {
            _pool = pool;
        }

        public void Execute(List<Entity> entities)
        {
            foreach (var entity in entities.Where(x => x.IsTile()))
            {
                AddOrUpdateBottomFor(entity);
            }
        }

        private void AddOrUpdateBottomFor(Entity entity)
        {
            _pool.CreateEntity()
                .SetParent(entity)
                .ReplacePosition(entity.position.Value)
                .ReplaceRotation(Random.Range(0, 4))
                .ReplaceResource("Bottoms/Empty");
        }
    }
}
