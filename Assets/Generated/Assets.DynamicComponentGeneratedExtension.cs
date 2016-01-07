namespace Entitas {
    public partial class Entity {
        static readonly Assets.DynamicComponent dynamicComponent = new Assets.DynamicComponent();

        public bool isDynamic {
            get { return HasComponent(ComponentIds.Dynamic); }
            set {
                if (value != isDynamic) {
                    if (value) {
                        AddComponent(ComponentIds.Dynamic, dynamicComponent);
                    } else {
                        RemoveComponent(ComponentIds.Dynamic);
                    }
                }
            }
        }

        public Entity IsDynamic(bool value) {
            isDynamic = value;
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherDynamic;

        public static IMatcher Dynamic {
            get {
                if (_matcherDynamic == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Dynamic);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherDynamic = matcher;
                }

                return _matcherDynamic;
            }
        }
    }
}
