using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public Assets.EntitasRefactor.ParentComponent parent { get { return (Assets.EntitasRefactor.ParentComponent)GetComponent(ComponentIds.Parent); } }

        public bool hasParent { get { return HasComponent(ComponentIds.Parent); } }

        static readonly Stack<Assets.EntitasRefactor.ParentComponent> _parentComponentPool = new Stack<Assets.EntitasRefactor.ParentComponent>();

        public static void ClearParentComponentPool() {
            _parentComponentPool.Clear();
        }

        public Entity AddParent(int newId) {
            var component = _parentComponentPool.Count > 0 ? _parentComponentPool.Pop() : new Assets.EntitasRefactor.ParentComponent();
            component.Id = newId;
            return AddComponent(ComponentIds.Parent, component);
        }

        public Entity ReplaceParent(int newId) {
            var previousComponent = hasParent ? parent : null;
            var component = _parentComponentPool.Count > 0 ? _parentComponentPool.Pop() : new Assets.EntitasRefactor.ParentComponent();
            component.Id = newId;
            ReplaceComponent(ComponentIds.Parent, component);
            if (previousComponent != null) {
                _parentComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveParent() {
            var component = parent;
            RemoveComponent(ComponentIds.Parent);
            _parentComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherParent;

        public static IMatcher Parent {
            get {
                if (_matcherParent == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Parent);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherParent = matcher;
                }

                return _matcherParent;
            }
        }
    }
}
