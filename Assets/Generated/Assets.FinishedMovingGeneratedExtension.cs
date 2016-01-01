namespace Entitas {
    public partial class Entity {
        static readonly Assets.FinishedMoving finishedMovingComponent = new Assets.FinishedMoving();

        public bool isFinishedMoving {
            get { return HasComponent(ComponentIds.FinishedMoving); }
            set {
                if (value != isFinishedMoving) {
                    if (value) {
                        AddComponent(ComponentIds.FinishedMoving, finishedMovingComponent);
                    } else {
                        RemoveComponent(ComponentIds.FinishedMoving);
                    }
                }
            }
        }

        public Entity IsFinishedMoving(bool value) {
            isFinishedMoving = value;
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherFinishedMoving;

        public static IMatcher FinishedMoving {
            get {
                if (_matcherFinishedMoving == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.FinishedMoving);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherFinishedMoving = matcher;
                }

                return _matcherFinishedMoving;
            }
        }
    }
}
