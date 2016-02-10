using System.Collections.Generic;

using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.LevelEditor.SelectedPlaceableComponent selectedPlaceable { get { return (Assets.LevelEditor.SelectedPlaceableComponent)GetComponent(GameComponentIds.SelectedPlaceable); } }

        public bool hasSelectedPlaceable { get { return HasComponent(GameComponentIds.SelectedPlaceable); } }

        static readonly Stack<Assets.LevelEditor.SelectedPlaceableComponent> _selectedPlaceableComponentPool = new Stack<Assets.LevelEditor.SelectedPlaceableComponent>();

        public static void ClearSelectedPlaceableComponentPool() {
            _selectedPlaceableComponentPool.Clear();
        }

        public Entity AddSelectedPlaceable(Assets.LevelEditor.Placeables.IPlaceable newValue) {
            var component = _selectedPlaceableComponentPool.Count > 0 ? _selectedPlaceableComponentPool.Pop() : new Assets.LevelEditor.SelectedPlaceableComponent();
            component.Value = newValue;
            return AddComponent(GameComponentIds.SelectedPlaceable, component);
        }

        public Entity ReplaceSelectedPlaceable(Assets.LevelEditor.Placeables.IPlaceable newValue) {
            var previousComponent = hasSelectedPlaceable ? selectedPlaceable : null;
            var component = _selectedPlaceableComponentPool.Count > 0 ? _selectedPlaceableComponentPool.Pop() : new Assets.LevelEditor.SelectedPlaceableComponent();
            component.Value = newValue;
            ReplaceComponent(GameComponentIds.SelectedPlaceable, component);
            if (previousComponent != null) {
                _selectedPlaceableComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveSelectedPlaceable() {
            var component = selectedPlaceable;
            RemoveComponent(GameComponentIds.SelectedPlaceable);
            _selectedPlaceableComponentPool.Push(component);
            return this;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherSelectedPlaceable;

        public static IMatcher SelectedPlaceable {
            get {
                if (_matcherSelectedPlaceable == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.SelectedPlaceable);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherSelectedPlaceable = matcher;
                }

                return _matcherSelectedPlaceable;
            }
        }
    }
