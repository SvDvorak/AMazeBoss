using Entitas;

namespace Entitas {
    public partial class Entity {
        static readonly Assets.LevelEditor.InputPlaceComponent inputPlaceComponent = new Assets.LevelEditor.InputPlaceComponent();

        public bool isInputPlace {
            get { return HasComponent(GameComponentIds.InputPlace); }
            set {
                if (value != isInputPlace) {
                    if (value) {
                        AddComponent(GameComponentIds.InputPlace, inputPlaceComponent);
                    } else {
                        RemoveComponent(GameComponentIds.InputPlace);
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
        public Entity inputPlaceEntity { get { return GetGroup(GameMatcher.InputPlace).GetSingleEntity(); } }

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
}

    public partial class GameMatcher {
        static IMatcher _matcherInputPlace;

        public static IMatcher InputPlace {
            get {
                if (_matcherInputPlace == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.InputPlace);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherInputPlace = matcher;
                }

                return _matcherInputPlace;
            }
        }
    }
