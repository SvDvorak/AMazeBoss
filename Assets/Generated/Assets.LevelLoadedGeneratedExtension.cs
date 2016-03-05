using Entitas;

namespace Entitas {
    public partial class Entity {
        static readonly Assets.LevelLoaded levelLoadedComponent = new Assets.LevelLoaded();

        public bool isLevelLoaded {
            get { return HasComponent(GameComponentIds.LevelLoaded); }
            set {
                if (value != isLevelLoaded) {
                    if (value) {
                        AddComponent(GameComponentIds.LevelLoaded, levelLoadedComponent);
                    } else {
                        RemoveComponent(GameComponentIds.LevelLoaded);
                    }
                }
            }
        }

        public Entity IsLevelLoaded(bool value) {
            isLevelLoaded = value;
            return this;
        }
    }

    public partial class Pool {
        public Entity levelLoadedEntity { get { return GetGroup(GameMatcher.LevelLoaded).GetSingleEntity(); } }

        public bool isLevelLoaded {
            get { return levelLoadedEntity != null; }
            set {
                var entity = levelLoadedEntity;
                if (value != (entity != null)) {
                    if (value) {
                        CreateEntity().isLevelLoaded = true;
                    } else {
                        DestroyEntity(entity);
                    }
                }
            }
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherLevelLoaded;

        public static IMatcher LevelLoaded {
            get {
                if (_matcherLevelLoaded == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.LevelLoaded);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherLevelLoaded = matcher;
                }

                return _matcherLevelLoaded;
            }
        }
    }
