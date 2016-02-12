using System.Collections.Generic;

using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.MainMenu.MenuItemComponent menuItem { get { return (Assets.MainMenu.MenuItemComponent)GetComponent(UiComponentIds.MenuItem); } }

        public bool hasMenuItem { get { return HasComponent(UiComponentIds.MenuItem); } }

        static readonly Stack<Assets.MainMenu.MenuItemComponent> _menuItemComponentPool = new Stack<Assets.MainMenu.MenuItemComponent>();

        public static void ClearMenuItemComponentPool() {
            _menuItemComponentPool.Clear();
        }

        public Entity AddMenuItem(string newText, UnityEngine.GameObject newParent) {
            var component = _menuItemComponentPool.Count > 0 ? _menuItemComponentPool.Pop() : new Assets.MainMenu.MenuItemComponent();
            component.Text = newText;
            component.Parent = newParent;
            return AddComponent(UiComponentIds.MenuItem, component);
        }

        public Entity ReplaceMenuItem(string newText, UnityEngine.GameObject newParent) {
            var previousComponent = hasMenuItem ? menuItem : null;
            var component = _menuItemComponentPool.Count > 0 ? _menuItemComponentPool.Pop() : new Assets.MainMenu.MenuItemComponent();
            component.Text = newText;
            component.Parent = newParent;
            ReplaceComponent(UiComponentIds.MenuItem, component);
            if (previousComponent != null) {
                _menuItemComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveMenuItem() {
            var component = menuItem;
            RemoveComponent(UiComponentIds.MenuItem);
            _menuItemComponentPool.Push(component);
            return this;
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
