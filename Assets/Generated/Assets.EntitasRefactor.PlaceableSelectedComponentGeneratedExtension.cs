using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public Assets.EntitasRefactor.PlaceableSelectedComponent placeableSelected { get { return (Assets.EntitasRefactor.PlaceableSelectedComponent)GetComponent(ComponentIds.PlaceableSelected); } }

        public bool hasPlaceableSelected { get { return HasComponent(ComponentIds.PlaceableSelected); } }

        static readonly Stack<Assets.EntitasRefactor.PlaceableSelectedComponent> _placeableSelectedComponentPool = new Stack<Assets.EntitasRefactor.PlaceableSelectedComponent>();

        public static void ClearPlaceableSelectedComponentPool() {
            _placeableSelectedComponentPool.Clear();
        }

        public Entity AddPlaceableSelected(Assets.EntitasRefactor.Placeables.IPlaceable newValue) {
            var component = _placeableSelectedComponentPool.Count > 0 ? _placeableSelectedComponentPool.Pop() : new Assets.EntitasRefactor.PlaceableSelectedComponent();
            component.Value = newValue;
            return AddComponent(ComponentIds.PlaceableSelected, component);
        }

        public Entity ReplacePlaceableSelected(Assets.EntitasRefactor.Placeables.IPlaceable newValue) {
            var previousComponent = hasPlaceableSelected ? placeableSelected : null;
            var component = _placeableSelectedComponentPool.Count > 0 ? _placeableSelectedComponentPool.Pop() : new Assets.EntitasRefactor.PlaceableSelectedComponent();
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
