namespace Entitas {
    public partial class Entity {
        static readonly Assets.LevelEditor.PausedComponent pausedComponent = new Assets.LevelEditor.PausedComponent();

        public bool isPaused {
            get { return HasComponent(ComponentIds.Paused); }
            set {
                if (value != isPaused) {
                    if (value) {
                        AddComponent(ComponentIds.Paused, pausedComponent);
                    } else {
                        RemoveComponent(ComponentIds.Paused);
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
        public Entity pausedEntity { get { return GetGroup(Matcher.Paused).GetSingleEntity(); } }

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

    public partial class Matcher {
        static IMatcher _matcherPaused;

        public static IMatcher Paused {
            get {
                if (_matcherPaused == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Paused);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherPaused = matcher;
                }

                return _matcherPaused;
            }
        }
    }
}
