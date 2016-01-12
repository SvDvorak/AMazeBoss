namespace Entitas {
    public partial class Entity {
        static readonly Assets.SpikesComponent spikesComponent = new Assets.SpikesComponent();

        public bool isSpikes {
            get { return HasComponent(ComponentIds.Spikes); }
            set {
                if (value != isSpikes) {
                    if (value) {
                        AddComponent(ComponentIds.Spikes, spikesComponent);
                    } else {
                        RemoveComponent(ComponentIds.Spikes);
                    }
                }
            }
        }

        public Entity IsSpikes(bool value) {
            isSpikes = value;
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherSpikes;

        public static IMatcher Spikes {
            get {
                if (_matcherSpikes == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Spikes);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherSpikes = matcher;
                }

                return _matcherSpikes;
            }
        }
    }
}
