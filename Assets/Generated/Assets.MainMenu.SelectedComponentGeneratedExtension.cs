using Entitas;

namespace Entitas {
    public partial class Entity {
        static readonly Assets.MainMenu.SelectedComponent selectedComponent = new Assets.MainMenu.SelectedComponent();

        public bool isSelected {
            get { return HasComponent(UiComponentIds.Selected); }
            set {
                if (value != isSelected) {
                    if (value) {
                        AddComponent(UiComponentIds.Selected, selectedComponent);
                    } else {
                        RemoveComponent(UiComponentIds.Selected);
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

    public partial class UiMatcher {
        static IMatcher _matcherSelected;

        public static IMatcher Selected {
            get {
                if (_matcherSelected == null) {
                    var matcher = (Matcher)Matcher.AllOf(UiComponentIds.Selected);
                    matcher.componentNames = UiComponentIds.componentNames;
                    _matcherSelected = matcher;
                }

                return _matcherSelected;
            }
        }
    }
