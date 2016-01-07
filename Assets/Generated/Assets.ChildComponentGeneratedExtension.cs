using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public Assets.ChildComponent child { get { return (Assets.ChildComponent)GetComponent(ComponentIds.Child); } }

        public bool hasChild { get { return HasComponent(ComponentIds.Child); } }

        static readonly Stack<Assets.ChildComponent> _childComponentPool = new Stack<Assets.ChildComponent>();

        public static void ClearChildComponentPool() {
            _childComponentPool.Clear();
        }

        public Entity AddChild(int newParentId) {
            var component = _childComponentPool.Count > 0 ? _childComponentPool.Pop() : new Assets.ChildComponent();
            component.ParentId = newParentId;
            return AddComponent(ComponentIds.Child, component);
        }

        public Entity ReplaceChild(int newParentId) {
            var previousComponent = hasChild ? child : null;
            var component = _childComponentPool.Count > 0 ? _childComponentPool.Pop() : new Assets.ChildComponent();
            component.ParentId = newParentId;
            ReplaceComponent(ComponentIds.Child, component);
            if (previousComponent != null) {
                _childComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveChild() {
            var component = child;
            RemoveComponent(ComponentIds.Child);
            _childComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherChild;

        public static IMatcher Child {
            get {
                if (_matcherChild == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Child);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherChild = matcher;
                }

                return _matcherChild;
            }
        }
    }
}
