using Entitas;

namespace Entitas {
    public partial class Entity {
        static readonly Assets.SpikesCarriedComponent spikesCarriedComponent = new Assets.SpikesCarriedComponent();

        public bool isSpikesCarried {
            get { return HasComponent(GameComponentIds.SpikesCarried); }
            set {
                if (value != isSpikesCarried) {
                    if (value) {
                        AddComponent(GameComponentIds.SpikesCarried, spikesCarriedComponent);
                    } else {
                        RemoveComponent(GameComponentIds.SpikesCarried);
                    }
                }
            }
        }

        public Entity IsSpikesCarried(bool value) {
            isSpikesCarried = value;
            return this;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherSpikesCarried;

        public static IMatcher SpikesCarried {
            get {
                if (_matcherSpikesCarried == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.SpikesCarried);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherSpikesCarried = matcher;
                }

                return _matcherSpikesCarried;
            }
        }
    }
