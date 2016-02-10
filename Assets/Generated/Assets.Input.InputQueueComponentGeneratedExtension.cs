using System.Collections.Generic;

using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.Input.InputQueueComponent inputQueue { get { return (Assets.Input.InputQueueComponent)GetComponent(GameComponentIds.InputQueue); } }

        public bool hasInputQueue { get { return HasComponent(GameComponentIds.InputQueue); } }

        static readonly Stack<Assets.Input.InputQueueComponent> _inputQueueComponentPool = new Stack<Assets.Input.InputQueueComponent>();

        public static void ClearInputQueueComponentPool() {
            _inputQueueComponentPool.Clear();
        }

        public Entity AddInputQueue(System.Action newInputAction) {
            var component = _inputQueueComponentPool.Count > 0 ? _inputQueueComponentPool.Pop() : new Assets.Input.InputQueueComponent();
            component.InputAction = newInputAction;
            return AddComponent(GameComponentIds.InputQueue, component);
        }

        public Entity ReplaceInputQueue(System.Action newInputAction) {
            var previousComponent = hasInputQueue ? inputQueue : null;
            var component = _inputQueueComponentPool.Count > 0 ? _inputQueueComponentPool.Pop() : new Assets.Input.InputQueueComponent();
            component.InputAction = newInputAction;
            ReplaceComponent(GameComponentIds.InputQueue, component);
            if (previousComponent != null) {
                _inputQueueComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveInputQueue() {
            var component = inputQueue;
            RemoveComponent(GameComponentIds.InputQueue);
            _inputQueueComponentPool.Push(component);
            return this;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherInputQueue;

        public static IMatcher InputQueue {
            get {
                if (_matcherInputQueue == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.InputQueue);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherInputQueue = matcher;
                }

                return _matcherInputQueue;
            }
        }
    }
