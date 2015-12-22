using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public Assets.EntitasRefactor.ItemComponent item { get { return (Assets.EntitasRefactor.ItemComponent)GetComponent(ComponentIds.Item); } }

        public bool hasItem { get { return HasComponent(ComponentIds.Item); } }

        static readonly Stack<Assets.EntitasRefactor.ItemComponent> _itemComponentPool = new Stack<Assets.EntitasRefactor.ItemComponent>();

        public static void ClearItemComponentPool() {
            _itemComponentPool.Clear();
        }

        public Entity AddItem(Assets.ItemType newType) {
            var component = _itemComponentPool.Count > 0 ? _itemComponentPool.Pop() : new Assets.EntitasRefactor.ItemComponent();
            component.Type = newType;
            return AddComponent(ComponentIds.Item, component);
        }

        public Entity ReplaceItem(Assets.ItemType newType) {
            var previousComponent = hasItem ? item : null;
            var component = _itemComponentPool.Count > 0 ? _itemComponentPool.Pop() : new Assets.EntitasRefactor.ItemComponent();
            component.Type = newType;
            ReplaceComponent(ComponentIds.Item, component);
            if (previousComponent != null) {
                _itemComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveItem() {
            var component = item;
            RemoveComponent(ComponentIds.Item);
            _itemComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherItem;

        public static IMatcher Item {
            get {
                if (_matcherItem == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Item);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherItem = matcher;
                }

                return _matcherItem;
            }
        }
    }
}
