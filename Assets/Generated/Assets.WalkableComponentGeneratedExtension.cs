namespace Entitas {
    public partial class Entity {
        static readonly Assets.WalkableComponent walkableComponent = new Assets.WalkableComponent();

        public bool isWalkable {
            get { return HasComponent(ComponentIds.Walkable); }
            set {
                if (value != isWalkable) {
                    if (value) {
                        AddComponent(ComponentIds.Walkable, walkableComponent);
                    } else {
                        RemoveComponent(ComponentIds.Walkable);
                    }
                }
            }
        }

        public Entity IsWalkable(bool value) {
            isWalkable = value;
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherWalkable;

        public static IMatcher Walkable {
            get {
                if (_matcherWalkable == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Walkable);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherWalkable = matcher;
                }

                return _matcherWalkable;
            }
        }
    }
}
