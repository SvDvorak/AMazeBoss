using Entitas;

namespace Entitas {
    public partial class Entity {
        static readonly Assets.DestroyedComponent destroyedComponent = new Assets.DestroyedComponent();

        public bool isDestroyed {
            get { return HasComponent(GameComponentIds.Destroyed); }
            set {
                if (value != isDestroyed) {
                    if (value) {
                        AddComponent(GameComponentIds.Destroyed, destroyedComponent);
                    } else {
                        RemoveComponent(GameComponentIds.Destroyed);
                    }
                }
            }
        }

        public Entity IsDestroyed(bool value) {
            isDestroyed = value;
            return this;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherDestroyed;

        public static IMatcher Destroyed {
            get {
                if (_matcherDestroyed == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.Destroyed);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherDestroyed = matcher;
                }

                return _matcherDestroyed;
            }
        }
    }
