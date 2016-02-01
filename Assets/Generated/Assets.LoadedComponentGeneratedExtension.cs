using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public Assets.LoadedComponent loaded { get { return (Assets.LoadedComponent)GetComponent(ComponentIds.Loaded); } }

        public bool hasLoaded { get { return HasComponent(ComponentIds.Loaded); } }

        static readonly Stack<Assets.LoadedComponent> _loadedComponentPool = new Stack<Assets.LoadedComponent>();

        public static void ClearLoadedComponentPool() {
            _loadedComponentPool.Clear();
        }

        public Entity AddLoaded(bool newLoadedThisTurn) {
            var component = _loadedComponentPool.Count > 0 ? _loadedComponentPool.Pop() : new Assets.LoadedComponent();
            component.LoadedThisTurn = newLoadedThisTurn;
            return AddComponent(ComponentIds.Loaded, component);
        }

        public Entity ReplaceLoaded(bool newLoadedThisTurn) {
            var previousComponent = hasLoaded ? loaded : null;
            var component = _loadedComponentPool.Count > 0 ? _loadedComponentPool.Pop() : new Assets.LoadedComponent();
            component.LoadedThisTurn = newLoadedThisTurn;
            ReplaceComponent(ComponentIds.Loaded, component);
            if (previousComponent != null) {
                _loadedComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveLoaded() {
            var component = loaded;
            RemoveComponent(ComponentIds.Loaded);
            _loadedComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherLoaded;

        public static IMatcher Loaded {
            get {
                if (_matcherLoaded == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Loaded);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherLoaded = matcher;
                }

                return _matcherLoaded;
            }
        }
    }
}
