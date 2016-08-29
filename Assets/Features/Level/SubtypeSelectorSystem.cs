using System.Collections.Generic;
using Entitas;

namespace Assets.Level
{
    public class SubtypeSelectorSystem : IReactiveSystem, ISetPool, IExcludeComponents
    {
        private Pool _pool;

        public TriggerOnEvent trigger { get { return GameMatcher.Maintype.OnEntityAdded(); } }
        public IMatcher excludeComponents { get { return GameMatcher.Subtype; } }

        public void SetPool(Pool pool)
        {
            _pool = pool;
        }

        public void Execute(List<Entity> entities)
        {
            foreach (var entity in entities)
            {
                var selectedSubtype = _pool.tileTemplates.Value.Retrieve(entity.maintype.Value);
                entity.AddSubtype(selectedSubtype.Item1);
            }
        }
    }
}