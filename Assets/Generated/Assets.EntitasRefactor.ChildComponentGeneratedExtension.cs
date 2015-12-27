using System.Collections.Generic;
using Assets;

namespace Entitas {
    public partial class Entity {
        public ChildComponent child { get { return (ChildComponent)GetComponent(ComponentIds.Child); } }

        public bool hasChild { get { return HasComponent(ComponentIds.Child); } }

        static readonly Stack<ChildComponent> _childComponentPool = new Stack<ChildComponent>();

        public static void ClearChildComponentPool() {
            _childComponentPool.Clear();
        }

        public Entity AddChild(int newParentId) {
            var component = _childComponentPool.Count > 0 ? _childComponentPool.Pop() : new ChildComponent();
            component.ParentId = newParentId;
            return AddComponent(ComponentIds.Child, component);
        }

        public Entity ReplaceChild(int newParentId) {
            var previousComponent = hasChild ? child : null;
            var component = _childComponentPool.Count > 0 ? _childComponentPool.Pop() : new ChildComponent();
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
