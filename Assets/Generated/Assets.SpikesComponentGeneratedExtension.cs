using Entitas;

namespace Entitas {
    public partial class Entity {
        static readonly Assets.SpikesComponent spikesComponent = new Assets.SpikesComponent();

        public bool isSpikes {
            get { return HasComponent(GameComponentIds.Spikes); }
            set {
                if (value != isSpikes) {
                    if (value) {
                        AddComponent(GameComponentIds.Spikes, spikesComponent);
                    } else {
                        RemoveComponent(GameComponentIds.Spikes);
                    }
                }
            }
        }

        public Entity IsSpikes(bool value) {
            isSpikes = value;
            return this;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherSpikes;

        public static IMatcher Spikes {
            get {
                if (_matcherSpikes == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.Spikes);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherSpikes = matcher;
                }

                return _matcherSpikes;
            }
        }
    }
