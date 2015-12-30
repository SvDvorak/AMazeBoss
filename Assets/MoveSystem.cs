using System.Collections.Generic;
using DG.Tweening;
using Entitas;

namespace Assets
{
    public class MoveSystem : IReactiveSystem
    {
        public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.View, Matcher.Position).OnEntityAdded(); } }

        public void Execute(List<Entity> entities)
        {
            foreach (var moving in entities)
            {
                var transform = moving.view.Value.transform;
                transform.DOMove(moving.position.Value.ToV3(), 1).SetEase(Ease.Linear);
            }
        }
    }
}
