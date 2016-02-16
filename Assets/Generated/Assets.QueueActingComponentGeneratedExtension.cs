using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.QueueActingComponent queueActing { get { return (Assets.QueueActingComponent)GetComponent(GameComponentIds.QueueActing); } }

        public bool hasQueueActing { get { return HasComponent(GameComponentIds.QueueActing); } }

        public Entity AddQueueActing(float newTime, System.Action newAction) {
            var componentPool = GetComponentPool(GameComponentIds.QueueActing);
            var component = (Assets.QueueActingComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.QueueActingComponent());
            component.Time = newTime;
            component.Action = newAction;
            return AddComponent(GameComponentIds.QueueActing, component);
        }

        public Entity ReplaceQueueActing(float newTime, System.Action newAction) {
            var componentPool = GetComponentPool(GameComponentIds.QueueActing);
            var component = (Assets.QueueActingComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.QueueActingComponent());
            component.Time = newTime;
            component.Action = newAction;
            ReplaceComponent(GameComponentIds.QueueActing, component);
            return this;
        }

        public Entity RemoveQueueActing() {
            return RemoveComponent(GameComponentIds.QueueActing);;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherQueueActing;

        public static IMatcher QueueActing {
            get {
                if (_matcherQueueActing == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.QueueActing);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherQueueActing = matcher;
                }

                return _matcherQueueActing;
            }
        }
    }
