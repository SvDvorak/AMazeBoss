namespace Entitas {
    public partial class Entity {
        static readonly Assets.Cursed cursedComponent = new Assets.Cursed();

        public bool isCursed {
            get { return HasComponent(ComponentIds.Cursed); }
            set {
                if (value != isCursed) {
                    if (value) {
                        AddComponent(ComponentIds.Cursed, cursedComponent);
                    } else {
                        RemoveComponent(ComponentIds.Cursed);
                    }
                }
            }
        }

        public Entity IsCursed(bool value) {
            isCursed = value;
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherCursed;

        public static IMatcher Cursed {
            get {
                if (_matcherCursed == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Cursed);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherCursed = matcher;
                }

                return _matcherCursed;
            }
        }
    }
}
