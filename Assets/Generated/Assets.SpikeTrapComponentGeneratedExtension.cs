namespace Entitas {
    public partial class Entity {
        static readonly Assets.SpikeTrapComponent spikeTrapComponent = new Assets.SpikeTrapComponent();

        public bool isSpikeTrap {
            get { return HasComponent(ComponentIds.SpikeTrap); }
            set {
                if (value != isSpikeTrap) {
                    if (value) {
                        AddComponent(ComponentIds.SpikeTrap, spikeTrapComponent);
                    } else {
                        RemoveComponent(ComponentIds.SpikeTrap);
                    }
                }
            }
        }

        public Entity IsSpikeTrap(bool value) {
            isSpikeTrap = value;
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
