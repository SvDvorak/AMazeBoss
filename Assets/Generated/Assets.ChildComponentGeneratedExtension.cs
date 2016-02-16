using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.ChildComponent child { get { return (Assets.ChildComponent)GetComponent(GameComponentIds.Child); } }

        public bool hasChild { get { return HasComponent(GameComponentIds.Child); } }

        public Entity AddChild(int newParentId) {
            var componentPool = GetComponentPool(GameComponentIds.Child);
            var component = (Assets.ChildComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.ChildComponent());
            component.ParentId = newParentId;
            return AddComponent(GameComponentIds.Child, component);
        }

        public Entity ReplaceChild(int newParentId) {
            var componentPool = GetComponentPool(GameComponentIds.Child);
            var component = (Assets.ChildComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.ChildComponent());
            component.ParentId = newParentId;
            ReplaceComponent(GameComponentIds.Child, component);
            return this;
        }

        public Entity RemoveChild() {
            return RemoveComponent(GameComponentIds.Child);;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherChild;

        public static IMatcher Child {
            get {
                if (_matcherChild == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.Child);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherChild = matcher;
                }

                return _matcherChild;
            }
        }
    }

    public partial class UiMatcher {
        static IMatcher _matcherChild;

        public static IMatcher Child {
            get {
                if (_matcherChild == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.Child);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherChild = matcher;
                }

                return _matcherChild;
            }
        }
    }
