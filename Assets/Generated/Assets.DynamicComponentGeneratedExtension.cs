using Entitas;

namespace Entitas {
    public partial class Entity {
        static readonly Assets.DynamicComponent dynamicComponent = new Assets.DynamicComponent();

        public bool isDynamic {
            get { return HasComponent(GameComponentIds.Dynamic); }
            set {
                if (value != isDynamic) {
                    if (value) {
                        AddComponent(GameComponentIds.Dynamic, dynamicComponent);
                    } else {
                        RemoveComponent(GameComponentIds.Dynamic);
                    }
                }
            }
        }

        public Entity IsDynamic(bool value) {
            isDynamic = value;
            return this;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherDynamic;

        public static IMatcher Dynamic {
            get {
                if (_matcherDynamic == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.Dynamic);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherDynamic = matcher;
                }

                return _matcherDynamic;
            }
        }
    }
