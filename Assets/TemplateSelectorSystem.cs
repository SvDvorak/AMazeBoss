using System.Collections.Generic;
using Entitas;
using Random = UnityEngine.Random;

namespace Assets
{
    public class TemplateSelectorSystem : IMultiReactiveSystem, ISetPool
    {
        private Pool _pool;

        public TriggerOnEvent[] triggers
        {
            get { return new[] { Matcher.Maintype.OnEntityAdded(), Matcher.Subtype.OnEntityAdded() }; }
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
            List<string> templateNames;
            if (entity.hasSubtype)
            {
                templateNames = _pool.tileTemplates.Value.Retrieve(entity.maintype.Value, entity.subtype.Value);
            }
            else
            {
                var selectedSubtype = _pool.tileTemplates.Value.Retrieve(entity.maintype.Value);
                entity.AddSubtype(selectedSubtype.Item1);
                templateNames = selectedSubtype.Item2;
            }

            return templateNames[Random.Range(0, templateNames.Count)];
        }
    }
}