using System.Collections.Generic;

using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.MainMenu.MenuItem menuItem { get { return (Assets.MainMenu.MenuItem)GetComponent(MenuComponentIds.MenuItem); } }

        public bool hasMenuItem { get { return HasComponent(MenuComponentIds.MenuItem); } }

        static readonly Stack<Assets.MainMenu.MenuItem> _menuItemComponentPool = new Stack<Assets.MainMenu.MenuItem>();

        public static void ClearMenuItemComponentPool() {
            _menuItemComponentPool.Clear();
        }

        public Entity AddMenuItem(string newText, UnityEngine.GameObject newParent) {
            var component = _menuItemComponentPool.Count > 0 ? _menuItemComponentPool.Pop() : new Assets.MainMenu.MenuItem();
            component.Text = newText;
            component.Parent = newParent;
            return AddComponent(MenuComponentIds.MenuItem, component);
        }

        public Entity ReplaceMenuItem(string newText, UnityEngine.GameObject newParent) {
            var previousComponent = hasMenuItem ? menuItem : null;
            var component = _menuItemComponentPool.Count > 0 ? _menuItemComponentPool.Pop() : new Assets.MainMenu.MenuItem();
            component.Text = newText;
            component.Parent = newParent;
            ReplaceComponent(MenuComponentIds.MenuItem, component);
            if (previousComponent != null) {
                _menuItemComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveMenuItem() {
            var component = menuItem;
            RemoveComponent(MenuComponentIds.MenuItem);
            _menuItemComponentPool.Push(component);
            return this;
        }
    }
}

    public partial class MenuMatcher {
        static IMatcher _matcherMenuItem;

        public static IMatcher MenuItem {
            get {
                if (_matcherMenuItem == null) {
                    var matcher = (Matcher)Matcher.AllOf(MenuComponentIds.MenuItem);
                    matcher.componentNames = MenuComponentIds.componentNames;
                    _matcherMenuItem = matcher;
                }

                return _matcherMenuItem;
            }
        }
    }
