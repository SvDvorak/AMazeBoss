using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public Assets.Input.InputMoveComponent inputMove { get { return (Assets.Input.InputMoveComponent)GetComponent(ComponentIds.InputMove); } }

        public bool hasInputMove { get { return HasComponent(ComponentIds.InputMove); } }

        static readonly Stack<Assets.Input.InputMoveComponent> _inputMoveComponentPool = new Stack<Assets.Input.InputMoveComponent>();

        public static void ClearInputMoveComponentPool() {
            _inputMoveComponentPool.Clear();
        }

        public Entity AddInputMove(Assets.TilePos newDirection) {
            var component = _inputMoveComponentPool.Count > 0 ? _inputMoveComponentPool.Pop() : new Assets.Input.InputMoveComponent();
            component.Direction = newDirection;
            return AddComponent(ComponentIds.InputMove, component);
        }

        public Entity ReplaceInputMove(Assets.TilePos newDirection) {
            var previousComponent = hasInputMove ? inputMove : null;
            var component = _inputMoveComponentPool.Count > 0 ? _inputMoveComponentPool.Pop() : new Assets.Input.InputMoveComponent();
            component.Direction = newDirection;
            ReplaceComponent(ComponentIds.InputMove, component);
            if (previousComponent != null) {
                _inputMoveComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveInputMove() {
            var component = inputMove;
            RemoveComponent(ComponentIds.InputMove);
            _inputMoveComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherInputMove;

        public static IMatcher InputMove {
            get {
                if (_matcherInputMove == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.InputMove);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherInputMove = matcher;
                }

                return _matcherInputMove;
            }
        }
    }
}
