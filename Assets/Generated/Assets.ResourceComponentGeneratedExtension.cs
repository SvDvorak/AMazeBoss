using System.Collections.Generic;

using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.ResourceComponent resource { get { return (Assets.ResourceComponent)GetComponent(GameComponentIds.Resource); } }

        public bool hasResource { get { return HasComponent(GameComponentIds.Resource); } }

        static readonly Stack<Assets.ResourceComponent> _resourceComponentPool = new Stack<Assets.ResourceComponent>();

        public static void ClearResourceComponentPool() {
            _resourceComponentPool.Clear();
        }

        public Entity AddResource(string newPath) {
            var component = _resourceComponentPool.Count > 0 ? _resourceComponentPool.Pop() : new Assets.ResourceComponent();
            component.Path = newPath;
            return AddComponent(GameComponentIds.Resource, component);
        }

        public Entity ReplaceResource(string newPath) {
            var previousComponent = hasResource ? resource : null;
            var component = _resourceComponentPool.Count > 0 ? _resourceComponentPool.Pop() : new Assets.ResourceComponent();
            component.Path = newPath;
            ReplaceComponent(GameComponentIds.Resource, component);
            if (previousComponent != null) {
                _resourceComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveResource() {
            var component = resource;
            RemoveComponent(GameComponentIds.Resource);
            _resourceComponentPool.Push(component);
            return this;
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
