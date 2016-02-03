using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public Assets.LevelEditor.SelectedPlaceableComponent selectedPlaceable { get { return (Assets.LevelEditor.SelectedPlaceableComponent)GetComponent(ComponentIds.SelectedPlaceable); } }

        public bool hasSelectedPlaceable { get { return HasComponent(ComponentIds.SelectedPlaceable); } }

        static readonly Stack<Assets.LevelEditor.SelectedPlaceableComponent> _selectedPlaceableComponentPool = new Stack<Assets.LevelEditor.SelectedPlaceableComponent>();

        public static void ClearSelectedPlaceableComponentPool() {
            _selectedPlaceableComponentPool.Clear();
        }

        public Entity AddSelectedPlaceable(Assets.LevelEditor.Placeables.IPlaceable newValue) {
            var component = _selectedPlaceableComponentPool.Count > 0 ? _selectedPlaceableComponentPool.Pop() : new Assets.LevelEditor.SelectedPlaceableComponent();
            component.Value = newValue;
            return AddComponent(ComponentIds.SelectedPlaceable, component);
        }

        public Entity ReplaceSelectedPlaceable(Assets.LevelEditor.Placeables.IPlaceable newValue) {
            var previousComponent = hasSelectedPlaceable ? selectedPlaceable : null;
            var component = _selectedPlaceableComponentPool.Count > 0 ? _selectedPlaceableComponentPool.Pop() : new Assets.LevelEditor.SelectedPlaceableComponent();
            component.Value = newValue;
            ReplaceComponent(ComponentIds.SelectedPlaceable, component);
            if (previousComponent != null) {
                _selectedPlaceableComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveSelectedPlaceable() {
            var component = selectedPlaceable;
            RemoveComponent(ComponentIds.SelectedPlaceable);
            _selectedPlaceableComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherSelectedPlaceable;

        public static IMatcher SelectedPlaceable {
            get {
                if (_matcherSelectedPlaceable == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.SelectedPlaceable);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherSelectedPlaceable = matcher;
                }

                return _matcherSelectedPlaceable;
            }
        }
    }
}
