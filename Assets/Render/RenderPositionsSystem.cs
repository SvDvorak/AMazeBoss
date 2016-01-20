using System.Collections.Generic;
using Entitas;

namespace Assets.Render
{
    public class RenderPositionsSystem : IReactiveSystem
    {
        public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.Position, Matcher.View).OnEntityAdded(); } }

        public void Execute(List<Entity> entities)
        {
            foreach (var entity in entities)
            {
                entity.view.Value.transform.position = entity.position.Value.ToV3() + entity.viewOffset.Value;
            }
        }
    }
}
