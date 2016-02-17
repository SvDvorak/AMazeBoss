using Entitas;

namespace Entitas {
    public partial class Entity {
        static readonly Assets.HeroComponent heroComponent = new Assets.HeroComponent();

        public bool isHero {
            get { return HasComponent(GameComponentIds.Hero); }
            set {
                if (value != isHero) {
                    if (value) {
                        AddComponent(GameComponentIds.Hero, heroComponent);
                    } else {
                        RemoveComponent(GameComponentIds.Hero);
                    }
                }
            }
        }

        public Entity IsHero(bool value) {
            isHero = value;
            return this;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherHero;

        public static IMatcher Hero {
            get {
                if (_matcherHero == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.Hero);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherHero = matcher;
                }

                return _matcherHero;
            }
        }
    }
