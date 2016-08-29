using Entitas;

namespace Assets.MainMenu
{
    public class CursorClickMenuItemSystem : IExecuteSystem, ISetPool
    {
        private Group _menuItemGroup;

        public void SetPool(Pool pool)
        {
            _menuItemGroup = pool.GetGroup(Matcher.AllOf(UiMatcher.ActivateAction, UiMatcher.View, UiMatcher.Selected));
        }

        public void Execute()
        {
            var clicked = UnityEngine.Input.GetMouseButtonDown(0);

            foreach (var menuItem in _menuItemGroup.GetEntities())
            {
                if (menuItem.isSelected && clicked)
                {
                    menuItem.activateAction.Action();
                }
            }
        }
    }
}
