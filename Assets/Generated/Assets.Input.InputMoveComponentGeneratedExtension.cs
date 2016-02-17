using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.Input.InputMoveComponent inputMove { get { return (Assets.Input.InputMoveComponent)GetComponent(GameComponentIds.InputMove); } }

        public bool hasInputMove { get { return HasComponent(GameComponentIds.InputMove); } }

        public Entity AddInputMove(Assets.TilePos newDirection) {
            var componentPool = GetComponentPool(GameComponentIds.InputMove);
            var component = (Assets.Input.InputMoveComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.Input.InputMoveComponent());
            component.Direction = newDirection;
            return AddComponent(GameComponentIds.InputMove, component);
        }

        public Entity ReplaceInputMove(Assets.TilePos newDirection) {
            var componentPool = GetComponentPool(GameComponentIds.InputMove);
            var component = (Assets.Input.InputMoveComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.Input.InputMoveComponent());
            component.Direction = newDirection;
            ReplaceComponent(GameComponentIds.InputMove, component);
            return this;
        }

        public Entity RemoveInputMove() {
            return RemoveComponent(GameComponentIds.InputMove);;
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
