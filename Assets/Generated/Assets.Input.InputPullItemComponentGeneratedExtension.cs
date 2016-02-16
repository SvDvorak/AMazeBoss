using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.Input.InputPullItemComponent inputPullItem { get { return (Assets.Input.InputPullItemComponent)GetComponent(GameComponentIds.InputPullItem); } }

        public bool hasInputPullItem { get { return HasComponent(GameComponentIds.InputPullItem); } }

        public Entity AddInputPullItem(Assets.TilePos newDirection) {
            var componentPool = GetComponentPool(GameComponentIds.InputPullItem);
            var component = (Assets.Input.InputPullItemComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.Input.InputPullItemComponent());
            component.Direction = newDirection;
            return AddComponent(GameComponentIds.InputPullItem, component);
        }

        public Entity ReplaceInputPullItem(Assets.TilePos newDirection) {
            var componentPool = GetComponentPool(GameComponentIds.InputPullItem);
            var component = (Assets.Input.InputPullItemComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.Input.InputPullItemComponent());
            component.Direction = newDirection;
            ReplaceComponent(GameComponentIds.InputPullItem, component);
            return this;
        }

        public Entity RemoveInputPullItem() {
            return RemoveComponent(GameComponentIds.InputPullItem);;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherInputPullItem;

        public static IMatcher InputPullItem {
            get {
                if (_matcherInputPullItem == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.InputPullItem);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherInputPullItem = matcher;
                }

                return _matcherInputPullItem;
            }
        }
    }
