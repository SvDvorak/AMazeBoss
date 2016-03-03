using System.Collections.Generic;
using DG.Tweening;
using Entitas;

namespace Assets.Render
{
    public class FinishedLevelLoadSystem : IReactiveSystem, ISetPool
    {
        private Group _animatorGroup;

        public TriggerOnEvent trigger { get { return GameMatcher.LevelLoaded.OnEntityAdded(); } }

        public void SetPool(Pool pool)
        {
            _animatorGroup = pool.GetGroup(GameMatcher.Animator);
        }

        public void Execute(List<Entity> entities)
        {
            var animatable = _animatorGroup.GetEntities();
            animatable.ForEach(x =>
            {
                if (x.hasActingActions)
                {
                    foreach (var actingAction in x.actingActions.Actions)
                    {
                        actingAction.Action.Complete(true);
                        x.animator.Value.Update(10000);
                    }
                    x.RemoveActingActions();
                }
                x.animator.Value.Update(10000);
            });
        }
    }
}
