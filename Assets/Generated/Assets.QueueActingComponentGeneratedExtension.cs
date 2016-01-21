using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public Assets.QueueActingComponent queueActing { get { return (Assets.QueueActingComponent)GetComponent(ComponentIds.QueueActing); } }

        public bool hasQueueActing { get { return HasComponent(ComponentIds.QueueActing); } }

        static readonly Stack<Assets.QueueActingComponent> _queueActingComponentPool = new Stack<Assets.QueueActingComponent>();

        public static void ClearQueueActingComponentPool() {
            _queueActingComponentPool.Clear();
        }

        public Entity AddQueueActing(float newTime, System.Action newAction) {
            var component = _queueActingComponentPool.Count > 0 ? _queueActingComponentPool.Pop() : new Assets.QueueActingComponent();
            component.Time = newTime;
            component.Action = newAction;
            return AddComponent(ComponentIds.QueueActing, component);
        }

        public Entity ReplaceQueueActing(float newTime, System.Action newAction) {
            var previousComponent = hasQueueActing ? queueActing : null;
            var component = _queueActingComponentPool.Count > 0 ? _queueActingComponentPool.Pop() : new Assets.QueueActingComponent();
            component.Time = newTime;
            component.Action = newAction;
            ReplaceComponent(ComponentIds.QueueActing, component);
            if (previousComponent != null) {
                _queueActingComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveQueueActing() {
            var component = queueActing;
            RemoveComponent(ComponentIds.QueueActing);
            _queueActingComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherQueueActing;

        public static IMatcher QueueActing {
            get {
                if (_matcherQueueActing == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.QueueActing);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherQueueActing = matcher;
                }

                return _matcherQueueActing;
            }
        }
    }
}
