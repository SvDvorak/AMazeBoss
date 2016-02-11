using System.Collections.Generic;
using DG.Tweening;
using Entitas;

namespace Assets.MainMenu
{
    public class SelectedItemAnimationSystem : IReactiveSystem
    {
        public TriggerOnEvent trigger { get { return MenuMatcher.Selected.OnEntityAddedOrRemoved(); } }

        public void Execute(List<Entity> entities)
        {
            foreach (var entity in entities)
            {
                var transform = entity.view.Value.transform;
                if(entity.isSelected)
                {
                    transform.DOScale(1.3f, 0.1f);
                }
                else
                {
                    transform.DOScale(1, 0.1f);
                }
            }
        }
    }
}