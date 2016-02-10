using System.Collections.Generic;

using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.Input.InputMoveComponent inputMove { get { return (Assets.Input.InputMoveComponent)GetComponent(GameComponentIds.InputMove); } }

        public bool hasInputMove { get { return HasComponent(GameComponentIds.InputMove); } }

        static readonly Stack<Assets.Input.InputMoveComponent> _inputMoveComponentPool = new Stack<Assets.Input.InputMoveComponent>();

        public static void ClearInputMoveComponentPool() {
            _inputMoveComponentPool.Clear();
        }

        public Entity AddInputMove(Assets.TilePos newDirection) {
            var component = _inputMoveComponentPool.Count > 0 ? _inputMoveComponentPool.Pop() : new Assets.Input.InputMoveComponent();
            component.Direction = newDirection;
            return AddComponent(GameComponentIds.InputMove, component);
        }

        public Entity ReplaceInputMove(Assets.TilePos newDirection) {
            var previousComponent = hasInputMove ? inputMove : null;
            var component = _inputMoveComponentPool.Count > 0 ? _inputMoveComponentPool.Pop() : new Assets.Input.InputMoveComponent();
            component.Direction = newDirection;
            ReplaceComponent(GameComponentIds.InputMove, component);
            if (previousComponent != null) {
                _inputMoveComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveInputMove() {
            var component = inputMove;
            RemoveComponent(GameComponentIds.InputMove);
            _inputMoveComponentPool.Push(component);
            return this;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherInputMove;

        public static IMatcher InputMove {
            get {
                if (_matcherInputMove == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.InputMove);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherInputMove = matcher;
                }

                return _matcherInputMove;
            }
        }
    }
