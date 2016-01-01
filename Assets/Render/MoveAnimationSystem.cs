using System.Collections.Generic;
using Entitas;

namespace Assets.Render
{
    public class MoveAnimationSystem : IMultiReactiveSystem, IEnsureComponents
    {
        public TriggerOnEvent[] triggers
        {
            get
            {
                return new[]
                    {
                        Matcher.Position.OnEntityAddedOrRemoved(),
                        Matcher.FinishedMoving.OnEntityAddedOrRemoved()
                    };
            }
        }

        public IMatcher ensureComponents { get { return Matcher.Animator; } }

        public void Execute(List<Entity> entities)
        {
            foreach (var e in entities)
            {
                e.animator.Value.SetBool("IsMoving", e.IsMoving());
            }
        }
    }
}
