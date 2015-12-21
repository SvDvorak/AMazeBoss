namespace Entitas {
    public partial class Entity {
        static readonly Assets.EntitasRefactor.DestroyedComponent destroyedComponent = new Assets.EntitasRefactor.DestroyedComponent();

        public bool isDestroyed {
            get { return HasComponent(ComponentIds.Destroyed); }
            set {
                if (value != isDestroyed) {
                    if (value) {
                        AddComponent(ComponentIds.Destroyed, destroyedComponent);
                    } else {
                        RemoveComponent(ComponentIds.Destroyed);
                    }
                }
            }
        }

        public Entity IsDestroyed(bool value) {
            isDestroyed = value;
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherDestroyed;

        public static IMatcher Destroyed {
            get {
                if (_matcherDestroyed == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Destroyed);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherDestroyed = matcher;
                }

                return _matcherDestroyed;
            }
        }
    }
}
