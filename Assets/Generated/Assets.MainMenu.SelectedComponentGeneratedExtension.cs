using Entitas;

namespace Entitas {
    public partial class Entity {
        static readonly Assets.MainMenu.SelectedComponent selectedComponent = new Assets.MainMenu.SelectedComponent();

        public bool isSelected {
            get { return HasComponent(MenuComponentIds.Selected); }
            set {
                if (value != isSelected) {
                    if (value) {
                        AddComponent(MenuComponentIds.Selected, selectedComponent);
                    } else {
                        RemoveComponent(MenuComponentIds.Selected);
                    }
                }
            }
        }

        public Entity IsSelected(bool value) {
            isSelected = value;
            return this;
        }
    }
}

    public partial class MenuMatcher {
        static IMatcher _matcherSelected;

        public static IMatcher Selected {
            get {
                if (_matcherSelected == null) {
                    var matcher = (Matcher)Matcher.AllOf(MenuComponentIds.Selected);
                    matcher.componentNames = MenuComponentIds.componentNames;
                    _matcherSelected = matcher;
                }

                return _matcherSelected;
            }
        }
    }
