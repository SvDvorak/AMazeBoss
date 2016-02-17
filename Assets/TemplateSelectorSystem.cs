using System.Collections.Generic;
using Entitas;
using Random = UnityEngine.Random;

namespace Assets
{
    public class TemplateSelectorSystem : IReactiveSystem, ISetPool
    {
        private Pool _pool;

        public TriggerOnEvent trigger
        {
            get { return Matcher.AllOf(GameMatcher.Maintype, GameMatcher.Subtype).OnEntityAdded(); }
        }

        public void SetPool(Pool pool)
        {
            _pool = pool;
        }

        public void Execute(List<Entity> entities)
        {
            foreach (var entity in entities)
            {
                entity.ReplaceResource(RetrieveTemplateName(entity));
            }
        }

        private string RetrieveTemplateName(Entity entity)
        {
            var templateNames = _pool.tileTemplates.Value.Retrieve(entity.maintype.Value, entity.subtype.Value);
            return templateNames[Random.Range(0, templateNames.Count)];
        }
    }
}