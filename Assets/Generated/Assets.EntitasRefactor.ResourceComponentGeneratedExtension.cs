using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public Assets.EntitasRefactor.ResourceComponent resource { get { return (Assets.EntitasRefactor.ResourceComponent)GetComponent(ComponentIds.Resource); } }

        public bool hasResource { get { return HasComponent(ComponentIds.Resource); } }

        static readonly Stack<Assets.EntitasRefactor.ResourceComponent> _resourceComponentPool = new Stack<Assets.EntitasRefactor.ResourceComponent>();

        public static void ClearResourceComponentPool() {
            _resourceComponentPool.Clear();
        }

        public Entity AddResource(string newPath) {
            var component = _resourceComponentPool.Count > 0 ? _resourceComponentPool.Pop() : new Assets.EntitasRefactor.ResourceComponent();
            component.Path = newPath;
            return AddComponent(ComponentIds.Resource, component);
        }

        public Entity ReplaceResource(string newPath) {
            var previousComponent = hasResource ? resource : null;
            var component = _resourceComponentPool.Count > 0 ? _resourceComponentPool.Pop() : new Assets.EntitasRefactor.ResourceComponent();
            component.Path = newPath;
            ReplaceComponent(ComponentIds.Resource, component);
            if (previousComponent != null) {
                _resourceComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveResource() {
            var component = resource;
            RemoveComponent(ComponentIds.Resource);
            _resourceComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherResource;

        public static IMatcher Resource {
            get {
                if (_matcherResource == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Resource);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherResource = matcher;
                }

                return _matcherResource;
            }
        }
    }
}
