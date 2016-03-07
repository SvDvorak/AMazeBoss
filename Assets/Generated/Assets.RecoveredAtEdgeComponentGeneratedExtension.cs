using Entitas;

namespace Entitas {
    public partial class Entity {
        static readonly Assets.RecoveredAtEdgeComponent recoveredAtEdgeComponent = new Assets.RecoveredAtEdgeComponent();

        public bool hasRecoveredAtEdge {
            get { return HasComponent(GameComponentIds.RecoveredAtEdge); }
            set {
                if (value != hasRecoveredAtEdge) {
                    if (value) {
                        AddComponent(GameComponentIds.RecoveredAtEdge, recoveredAtEdgeComponent);
                    } else {
                        RemoveComponent(GameComponentIds.RecoveredAtEdge);
                    }
                }
            }
        }

        public Entity HasRecoveredAtEdge(bool value) {
            hasRecoveredAtEdge = value;
            return this;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherRecoveredAtEdge;

        public static IMatcher RecoveredAtEdge {
            get {
                if (_matcherRecoveredAtEdge == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.RecoveredAtEdge);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherRecoveredAtEdge = matcher;
                }

                return _matcherRecoveredAtEdge;
            }
        }
    }
