using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.LevelEditor.SelectedPlaceableComponent selectedPlaceable { get { return (Assets.LevelEditor.SelectedPlaceableComponent)GetComponent(GameComponentIds.SelectedPlaceable); } }

        public bool hasSelectedPlaceable { get { return HasComponent(GameComponentIds.SelectedPlaceable); } }

        public Entity AddSelectedPlaceable(Assets.LevelEditor.Placeables.IPlaceable newValue) {
            var componentPool = GetComponentPool(GameComponentIds.SelectedPlaceable);
            var component = (Assets.LevelEditor.SelectedPlaceableComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.LevelEditor.SelectedPlaceableComponent());
            component.Value = newValue;
            return AddComponent(GameComponentIds.SelectedPlaceable, component);
        }

        public Entity ReplaceSelectedPlaceable(Assets.LevelEditor.Placeables.IPlaceable newValue) {
            var componentPool = GetComponentPool(GameComponentIds.SelectedPlaceable);
            var component = (Assets.LevelEditor.SelectedPlaceableComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.LevelEditor.SelectedPlaceableComponent());
            component.Value = newValue;
            ReplaceComponent(GameComponentIds.SelectedPlaceable, component);
            return this;
        }

        public Entity RemoveSelectedPlaceable() {
            return RemoveComponent(GameComponentIds.SelectedPlaceable);;
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
