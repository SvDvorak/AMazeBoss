using Entitas;

namespace Entitas {
    public partial class Entity {
        static readonly Assets.SpikeTrapComponent spikeTrapComponent = new Assets.SpikeTrapComponent();

        public bool isSpikeTrap {
            get { return HasComponent(GameComponentIds.SpikeTrap); }
            set {
                if (value != isSpikeTrap) {
                    if (value) {
                        AddComponent(GameComponentIds.SpikeTrap, spikeTrapComponent);
                    } else {
                        RemoveComponent(GameComponentIds.SpikeTrap);
                    }
                }
            }
        }

        public Entity IsSpikeTrap(bool value) {
            isSpikeTrap = value;
            return this;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherSpikeTrap;

        public static IMatcher SpikeTrap {
            get {
                if (_matcherSpikeTrap == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.SpikeTrap);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherSpikeTrap = matcher;
                }

                return _matcherSpikeTrap;
            }
        }
    }
