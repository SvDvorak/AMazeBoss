using System.Collections.Generic;
using System.Linq;
using Entitas;

namespace Assets.Render
{
    public class MoveAnimationSystem : IExecuteSystem, ISetPool
    {
        private Group _animatables;

        public void SetPool(Pool pool)
        {
            _animatables = pool.GetGroup(Matcher.Animator);
        }

        public void Execute()
        {
            foreach (var e in _animatables.GetEntities())
            {
                e.animator.Value.SetBool("IsMoving", e.hasActingTime && e.IsMoving());
            }
        }
    }
}