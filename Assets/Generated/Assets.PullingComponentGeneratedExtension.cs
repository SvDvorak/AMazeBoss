using Entitas;

namespace Entitas {
    public partial class Entity {
        static readonly Assets.PullingComponent pullingComponent = new Assets.PullingComponent();

        public bool isPulling {
            get { return HasComponent(GameComponentIds.Pulling); }
            set {
                if (value != isPulling) {
                    if (value) {
                        AddComponent(GameComponentIds.Pulling, pullingComponent);
                    } else {
                        RemoveComponent(GameComponentIds.Pulling);
                    }
                }
            }
        }

        public Entity IsPulling(bool value) {
            isPulling = value;
            return this;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherPulling;

        public static IMatcher Pulling {
            get {
                if (_matcherPulling == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.Pulling);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherPulling = matcher;
                }

                return _matcherPulling;
            }
        }
    }
