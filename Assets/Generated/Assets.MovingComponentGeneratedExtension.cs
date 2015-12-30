namespace Entitas {
    public partial class Entity {
        static readonly Assets.MovingComponent movingComponent = new Assets.MovingComponent();

        public bool isMoving {
            get { return HasComponent(ComponentIds.Moving); }
            set {
                if (value != isMoving) {
                    if (value) {
                        AddComponent(ComponentIds.Moving, movingComponent);
                    } else {
                        RemoveComponent(ComponentIds.Moving);
                    }
                }
            }
        }

        public Entity IsMoving(bool value) {
            isMoving = value;
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherMoving;

        public static IMatcher Moving {
            get {
                if (_matcherMoving == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Moving);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherMoving = matcher;
                }

                return _matcherMoving;
            }
        }
    }
}
