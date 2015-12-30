using System.Collections.Generic;
using Entitas;

namespace Assets.Render
{
    public class MoveAnimationSystem : IReactiveSystem, IEnsureComponents
    {
        public TriggerOnEvent trigger { get { return Matcher.Moving.OnEntityAddedOrRemoved(); } }
        public IMatcher ensureComponents { get { return Matcher.Animator; } }

        public void Execute(List<Entity> entities)
        {
            foreach (var e in entities)
            {
                e.animator.Value.SetBool("IsMoving", e.isMoving);
            }
        }
    }
}
