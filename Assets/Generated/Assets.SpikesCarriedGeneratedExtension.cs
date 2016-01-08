namespace Entitas {
    public partial class Entity {
        static readonly Assets.SpikesCarried spikesCarriedComponent = new Assets.SpikesCarried();

        public bool isSpikesCarried {
            get { return HasComponent(ComponentIds.SpikesCarried); }
            set {
                if (value != isSpikesCarried) {
                    if (value) {
                        AddComponent(ComponentIds.SpikesCarried, spikesCarriedComponent);
                    } else {
                        RemoveComponent(ComponentIds.SpikesCarried);
                    }
                }
            }
        }

        public Entity IsSpikesCarried(bool value) {
            isSpikesCarried = value;
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherSpikesCarried;

        public static IMatcher SpikesCarried {
            get {
                if (_matcherSpikesCarried == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.SpikesCarried);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherSpikesCarried = matcher;
                }

                return _matcherSpikesCarried;
            }
        }
    }
}
