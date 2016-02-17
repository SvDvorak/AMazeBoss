using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.MainMenu.MenuItemComponent menuItem { get { return (Assets.MainMenu.MenuItemComponent)GetComponent(UiComponentIds.MenuItem); } }

        public bool hasMenuItem { get { return HasComponent(UiComponentIds.MenuItem); } }

        public Entity AddMenuItem(string newText, UnityEngine.GameObject newParent) {
            var componentPool = GetComponentPool(UiComponentIds.MenuItem);
            var component = (Assets.MainMenu.MenuItemComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.MainMenu.MenuItemComponent());
            component.Text = newText;
            component.Parent = newParent;
            return AddComponent(UiComponentIds.MenuItem, component);
        }

        public Entity ReplaceMenuItem(string newText, UnityEngine.GameObject newParent) {
            var componentPool = GetComponentPool(UiComponentIds.MenuItem);
            var component = (Assets.MainMenu.MenuItemComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.MainMenu.MenuItemComponent());
            component.Text = newText;
            component.Parent = newParent;
            ReplaceComponent(UiComponentIds.MenuItem, component);
            return this;
        }

        public Entity RemoveMenuItem() {
            return RemoveComponent(UiComponentIds.MenuItem);;
        }
    }
}

    public partial class UiMatcher {
        static IMatcher _matcherMenuItem;

        public static IMatcher MenuItem {
            get {
                if (_matcherMenuItem == null) {
                    var matcher = (Matcher)Matcher.AllOf(UiComponentIds.MenuItem);
                    matcher.componentNames = UiComponentIds.componentNames;
                    _matcherMenuItem = matcher;
                }

                return _matcherMenuItem;
            }
        }
    }
