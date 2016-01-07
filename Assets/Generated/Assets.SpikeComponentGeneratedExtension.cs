namespace Entitas {
    public partial class Entity {
        static readonly Assets.SpikeComponent spikeComponent = new Assets.SpikeComponent();

        public bool isSpike {
            get { return HasComponent(ComponentIds.Spike); }
            set {
                if (value != isSpike) {
                    if (value) {
                        AddComponent(ComponentIds.Spike, spikeComponent);
                    } else {
                        RemoveComponent(ComponentIds.Spike);
                    }
                }
            }
        }

        public Entity IsSpike(bool value) {
            isSpike = value;
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherSpike;

        public static IMatcher Spike {
            get {
                if (_matcherSpike == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Spike);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherSpike = matcher;
                }

                return _matcherSpike;
            }
        }
    }
}
