using System.Collections.Generic;
using System.Text;
using Entitas;
using Assets;
using Random = UnityEngine.Random;

namespace Assets.EntitasRefactor
{
    public class TileTemplateSelectorSystem : IMultiReactiveSystem, ISetPool
    {
        private Pool _pool;

        public TriggerOnEvent[] triggers
        {
            get { return new[] { Matcher.Tile.OnEntityAdded(), Matcher.Subtype.OnEntityAdded() }; }
        }

        public void SetPool(Pool pool)
        {
            _pool = pool;
        }

        public void Execute(List<Entity> entities)
        {
            foreach (var entity in entities)
            {
                entity.ReplaceResource(RetrieveTileTemplateName(entity));
            }
        }

        private string RetrieveTileTemplateName(Entity entity)
        {
            List<string> templateNames;
            if (entity.hasSubtype)
            {
                templateNames = _pool.tileTemplates.Value.Retrieve(entity.tile.Type, entity.subtype.Value.ToUpper());
            }
            else
            {
                var selectedSubtype = _pool.tileTemplates.Value.Retrieve(entity.tile.Type);
                entity.AddSubtype(selectedSubtype.Item1);
                templateNames = selectedSubtype.Item2;
            }

            return templateNames[Random.Range(0, templateNames.Count)];
        }
    }
}