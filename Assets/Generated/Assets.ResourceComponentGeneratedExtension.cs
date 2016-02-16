using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.ResourceComponent resource { get { return (Assets.ResourceComponent)GetComponent(GameComponentIds.Resource); } }

        public bool hasResource { get { return HasComponent(GameComponentIds.Resource); } }

        public Entity AddResource(string newPath) {
            var componentPool = GetComponentPool(GameComponentIds.Resource);
            var component = (Assets.ResourceComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.ResourceComponent());
            component.Path = newPath;
            return AddComponent(GameComponentIds.Resource, component);
        }

        public Entity ReplaceResource(string newPath) {
            var componentPool = GetComponentPool(GameComponentIds.Resource);
            var component = (Assets.ResourceComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.ResourceComponent());
            component.Path = newPath;
            ReplaceComponent(GameComponentIds.Resource, component);
            return this;
        }

        public Entity RemoveResource() {
            return RemoveComponent(GameComponentIds.Resource);;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherResource;

        public static IMatcher Resource {
            get {
                if (_matcherResource == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.Resource);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherResource = matcher;
                }

                return _matcherResource;
            }
        }
    }

    public partial class UiMatcher {
        static IMatcher _matcherResource;

        public static IMatcher Resource {
            get {
                if (_matcherResource == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.Resource);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherResource = matcher;
                }

                return _matcherResource;
            }
        }
    }
