namespace Entitas {
    public partial class Entity {
        static readonly Assets.LevelEditor.InputPlaceComponent inputPlaceComponent = new Assets.LevelEditor.InputPlaceComponent();

        public bool isInputPlace {
            get { return HasComponent(ComponentIds.InputPlace); }
            set {
                if (value != isInputPlace) {
                    if (value) {
                        AddComponent(ComponentIds.InputPlace, inputPlaceComponent);
                    } else {
                        RemoveComponent(ComponentIds.InputPlace);
                    }
                }
            }
        }

        public Entity IsInputPlace(bool value) {
            isInputPlace = value;
            return this;
        }
    }

    public partial class Pool {
        public Entity inputPlaceEntity { get { return GetGroup(Matcher.InputPlace).GetSingleEntity(); } }

        public bool isInputPlace {
            get { return inputPlaceEntity != null; }
            set {
                var entity = inputPlaceEntity;
                if (value != (entity != null)) {
                    if (value) {
                        CreateEntity().isInputPlace = true;
                    } else {
                        DestroyEntity(entity);
                    }
                }
            }
        }
    }

    public partial class Matcher {
        static IMatcher _matcherInputPlace;

        public static IMatcher InputPlace {
            get {
                if (_matcherInputPlace == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.InputPlace);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherInputPlace = matcher;
                }

                return _matcherInputPlace;
            }
        }
    }
}
