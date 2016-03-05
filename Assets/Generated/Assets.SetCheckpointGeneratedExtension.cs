using Entitas;

namespace Entitas {
    public partial class Entity {
        static readonly Assets.SetCheckpoint setCheckpointComponent = new Assets.SetCheckpoint();

        public bool hasSetCheckpoint {
            get { return HasComponent(GameComponentIds.SetCheckpoint); }
            set {
                if (value != hasSetCheckpoint) {
                    if (value) {
                        AddComponent(GameComponentIds.SetCheckpoint, setCheckpointComponent);
                    } else {
                        RemoveComponent(GameComponentIds.SetCheckpoint);
                    }
                }
            }
        }

        public Entity HasSetCheckpoint(bool value) {
            hasSetCheckpoint = value;
            return this;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherSetCheckpoint;

        public static IMatcher SetCheckpoint {
            get {
                if (_matcherSetCheckpoint == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.SetCheckpoint);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherSetCheckpoint = matcher;
                }

                return _matcherSetCheckpoint;
            }
        }
    }
