using System.Collections.Generic;
using System.Linq;
using Entitas;

namespace Assets
{
    public class KnockBoxSystem : IReactiveSystem
    {
        public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.Box, Matcher.Knocked).OnEntityAdded(); } }

        public void Execute(List<Entity> entities)
        {
            foreach (var box in entities)
            {
                KnockBox(box, box.knocked.FromDirection);
            }
        }

        private void KnockBox(Entity entity, TilePos knockDirection)
        {
            entity.ReplacePosition(entity.position.Value + knockDirection);
        }
    }
}
