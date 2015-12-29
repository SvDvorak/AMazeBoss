namespace Entitas {
    public partial class Entity {
        static readonly Assets.IsAnimating isAnimatingComponent = new Assets.IsAnimating();

        public bool isIsAnimating {
            get { return HasComponent(ComponentIds.IsAnimating); }
            set {
                if (value != isIsAnimating) {
                    if (value) {
                        AddComponent(ComponentIds.IsAnimating, isAnimatingComponent);
                    } else {
                        RemoveComponent(ComponentIds.IsAnimating);
                    }
                }
            }
        }

        public Entity IsIsAnimating(bool value) {
            isIsAnimating = value;
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherIsAnimating;

        public static IMatcher IsAnimating {
            get {
                if (_matcherIsAnimating == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.IsAnimating);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherIsAnimating = matcher;
                }

                return _matcherIsAnimating;
            }
        }
    }
}
