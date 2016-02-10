using Entitas;

namespace Entitas {
    public partial class Entity {
        static readonly Assets.LevelEditor.PausedComponent pausedComponent = new Assets.LevelEditor.PausedComponent();

        public bool isPaused {
            get { return HasComponent(GameComponentIds.Paused); }
            set {
                if (value != isPaused) {
                    if (value) {
                        AddComponent(GameComponentIds.Paused, pausedComponent);
                    } else {
                        RemoveComponent(GameComponentIds.Paused);
                    }
                }
            }
        }

        public Entity IsPaused(bool value) {
            isPaused = value;
            return this;
        }
    }

    public partial class Pool {
        public Entity pausedEntity { get { return GetGroup(GameMatcher.Paused).GetSingleEntity(); } }

        public bool isPaused {
            get { return pausedEntity != null; }
            set {
                var entity = pausedEntity;
                if (value != (entity != null)) {
                    if (value) {
                        CreateEntity().isPaused = true;
                    } else {
                        DestroyEntity(entity);
                    }
                }
            }
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherPaused;

        public static IMatcher Paused {
            get {
                if (_matcherPaused == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.Paused);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherPaused = matcher;
                }

                return _matcherPaused;
            }
        }
    }
