using System.Collections.Generic;
using Assets.LevelEditor;
using Assets.LevelEditor.Placeables;

namespace Entitas {
    public partial class Entity {
        public PlaceableSelectedComponent placeableSelected { get { return (PlaceableSelectedComponent)GetComponent(ComponentIds.PlaceableSelected); } }

        public bool hasPlaceableSelected { get { return HasComponent(ComponentIds.PlaceableSelected); } }

        static readonly Stack<PlaceableSelectedComponent> _placeableSelectedComponentPool = new Stack<PlaceableSelectedComponent>();

        public static void ClearPlaceableSelectedComponentPool() {
            _placeableSelectedComponentPool.Clear();
        }

        public Entity AddPlaceableSelected(Placeable newValue) {
            var component = _placeableSelectedComponentPool.Count > 0 ? _placeableSelectedComponentPool.Pop() : new PlaceableSelectedComponent();
            component.Value = newValue;
            return AddComponent(ComponentIds.PlaceableSelected, component);
        }

        public Entity ReplacePlaceableSelected(Placeable newValue) {
            var previousComponent = hasPlaceableSelected ? placeableSelected : null;
            var component = _placeableSelectedComponentPool.Count > 0 ? _placeableSelectedComponentPool.Pop() : new PlaceableSelectedComponent();
            component.Value = newValue;
            ReplaceComponent(ComponentIds.PlaceableSelected, component);
            if (previousComponent != null) {
                _placeableSelectedComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemovePlaceableSelected() {
            var component = placeableSelected;
            RemoveComponent(ComponentIds.PlaceableSelected);
            _placeableSelectedComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherPlaceableSelected;

        public static IMatcher PlaceableSelected {
            get {
                if (_matcherPlaceableSelected == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.PlaceableSelected);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherPlaceableSelected = matcher;
                }

                return _matcherPlaceableSelected;
            }
        }
    }
}
