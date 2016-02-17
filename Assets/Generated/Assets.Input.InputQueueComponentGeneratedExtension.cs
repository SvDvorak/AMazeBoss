using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.Input.InputQueueComponent inputQueue { get { return (Assets.Input.InputQueueComponent)GetComponent(GameComponentIds.InputQueue); } }

        public bool hasInputQueue { get { return HasComponent(GameComponentIds.InputQueue); } }

        public Entity AddInputQueue(System.Action newInputAction) {
            var componentPool = GetComponentPool(GameComponentIds.InputQueue);
            var component = (Assets.Input.InputQueueComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.Input.InputQueueComponent());
            component.InputAction = newInputAction;
            return AddComponent(GameComponentIds.InputQueue, component);
        }

        public Entity ReplaceInputQueue(System.Action newInputAction) {
            var componentPool = GetComponentPool(GameComponentIds.InputQueue);
            var component = (Assets.Input.InputQueueComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.Input.InputQueueComponent());
            component.InputAction = newInputAction;
            ReplaceComponent(GameComponentIds.InputQueue, component);
            return this;
        }

        public Entity RemoveInputQueue() {
            return RemoveComponent(GameComponentIds.InputQueue);;
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
