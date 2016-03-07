using Entitas;

namespace Entitas {
    public partial class Entity {
        static readonly Assets.PushingComponent pushingComponent = new Assets.PushingComponent();

        public bool isPushing {
            get { return HasComponent(GameComponentIds.Pushing); }
            set {
                if (value != isPushing) {
                    if (value) {
                        AddComponent(GameComponentIds.Pushing, pushingComponent);
                    } else {
                        RemoveComponent(GameComponentIds.Pushing);
                    }
                }
            }
        }

        public Entity IsPushing(bool value) {
            isPushing = value;
            return this;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherPushing;

        public static IMatcher Pushing {
            get {
                if (_matcherPushing == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.Pushing);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherPushing = matcher;
                }

                return _matcherPushing;
            }
        }
    }
