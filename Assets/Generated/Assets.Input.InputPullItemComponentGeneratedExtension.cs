using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public Assets.Input.InputPullItemComponent inputPullItem { get { return (Assets.Input.InputPullItemComponent)GetComponent(ComponentIds.InputPullItem); } }

        public bool hasInputPullItem { get { return HasComponent(ComponentIds.InputPullItem); } }

        static readonly Stack<Assets.Input.InputPullItemComponent> _inputPullItemComponentPool = new Stack<Assets.Input.InputPullItemComponent>();

        public static void ClearInputPullItemComponentPool() {
            _inputPullItemComponentPool.Clear();
        }

        public Entity AddInputPullItem(Assets.TilePos newDirection) {
            var component = _inputPullItemComponentPool.Count > 0 ? _inputPullItemComponentPool.Pop() : new Assets.Input.InputPullItemComponent();
            component.Direction = newDirection;
            return AddComponent(ComponentIds.InputPullItem, component);
        }

        public Entity ReplaceInputPullItem(Assets.TilePos newDirection) {
            var previousComponent = hasInputPullItem ? inputPullItem : null;
            var component = _inputPullItemComponentPool.Count > 0 ? _inputPullItemComponentPool.Pop() : new Assets.Input.InputPullItemComponent();
            component.Direction = newDirection;
            ReplaceComponent(ComponentIds.InputPullItem, component);
            if (previousComponent != null) {
                _inputPullItemComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveInputPullItem() {
            var component = inputPullItem;
            RemoveComponent(ComponentIds.InputPullItem);
            _inputPullItemComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherInputPullItem;

        public static IMatcher InputPullItem {
            get {
                if (_matcherInputPullItem == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.InputPullItem);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherInputPullItem = matcher;
                }

                return _matcherInputPullItem;
            }
        }
    }
}
