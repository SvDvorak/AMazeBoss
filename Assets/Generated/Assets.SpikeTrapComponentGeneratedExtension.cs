using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public Assets.SpikeTrapComponent spikeTrap { get { return (Assets.SpikeTrapComponent)GetComponent(ComponentIds.SpikeTrap); } }

        public bool hasSpikeTrap { get { return HasComponent(ComponentIds.SpikeTrap); } }

        static readonly Stack<Assets.SpikeTrapComponent> _spikeTrapComponentPool = new Stack<Assets.SpikeTrapComponent>();

        public static void ClearSpikeTrapComponentPool() {
            _spikeTrapComponentPool.Clear();
        }

        public Entity AddSpikeTrap(bool newIsLoaded) {
            var component = _spikeTrapComponentPool.Count > 0 ? _spikeTrapComponentPool.Pop() : new Assets.SpikeTrapComponent();
            component.IsLoaded = newIsLoaded;
            return AddComponent(ComponentIds.SpikeTrap, component);
        }

        public Entity ReplaceSpikeTrap(bool newIsLoaded) {
            var previousComponent = hasSpikeTrap ? spikeTrap : null;
            var component = _spikeTrapComponentPool.Count > 0 ? _spikeTrapComponentPool.Pop() : new Assets.SpikeTrapComponent();
            component.IsLoaded = newIsLoaded;
            ReplaceComponent(ComponentIds.SpikeTrap, component);
            if (previousComponent != null) {
                _spikeTrapComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveSpikeTrap() {
            var component = spikeTrap;
            RemoveComponent(ComponentIds.SpikeTrap);
            _spikeTrapComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherSpikeTrap;

        public static IMatcher SpikeTrap {
            get {
                if (_matcherSpikeTrap == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.SpikeTrap);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherSpikeTrap = matcher;
                }

                return _matcherSpikeTrap;
            }
        }
    }
}
