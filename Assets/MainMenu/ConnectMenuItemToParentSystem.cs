using System.Collections.Generic;
using Entitas;

namespace Assets.MainMenu
{
    public class ConnectMenuItemToParentSystem : IReactiveSystem
    {
        public TriggerOnEvent trigger { get { return Matcher.AllOf(MenuMatcher.MenuItem, MenuMatcher.View).OnEntityAdded(); } }

        public void Execute(List<Entity> entities)
        {
            foreach (var entity in entities)
            {
                var childTransform = entity.view.Value.transform;
                var currentPosition = childTransform.localPosition;
                var parentTransform = entity.menuItem.Parent.transform;
                childTransform.SetParent(parentTransform);
                childTransform.localPosition = currentPosition;
            }
        }
    }
}