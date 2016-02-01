using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public Assets.Input.InputQueueComponent inputQueue { get { return (Assets.Input.InputQueueComponent)GetComponent(ComponentIds.InputQueue); } }

        public bool hasInputQueue { get { return HasComponent(ComponentIds.InputQueue); } }

        static readonly Stack<Assets.Input.InputQueueComponent> _inputQueueComponentPool = new Stack<Assets.Input.InputQueueComponent>();

        public static void ClearInputQueueComponentPool() {
            _inputQueueComponentPool.Clear();
        }

        public Entity AddInputQueue(System.Action newInputAction) {
            var component = _inputQueueComponentPool.Count > 0 ? _inputQueueComponentPool.Pop() : new Assets.Input.InputQueueComponent();
            component.InputAction = newInputAction;
            return AddComponent(ComponentIds.InputQueue, component);
        }

        public Entity ReplaceInputQueue(System.Action newInputAction) {
            var previousComponent = hasInputQueue ? inputQueue : null;
            var component = _inputQueueComponentPool.Count > 0 ? _inputQueueComponentPool.Pop() : new Assets.Input.InputQueueComponent();
            component.InputAction = newInputAction;
            ReplaceComponent(ComponentIds.InputQueue, component);
            if (previousComponent != null) {
                _inputQueueComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveInputQueue() {
            var component = inputQueue;
            RemoveComponent(ComponentIds.InputQueue);
            _inputQueueComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherInputQueue;

        public static IMatcher InputQueue {
            get {
                if (_matcherInputQueue == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.InputQueue);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherInputQueue = matcher;
                }

                return _matcherInputQueue;
            }
        }
    }
}
