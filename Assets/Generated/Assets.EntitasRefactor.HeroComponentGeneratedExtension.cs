using Assets;

namespace Entitas {
    public partial class Entity {
        static readonly HeroComponent heroComponent = new HeroComponent();

        public bool isHero {
            get { return HasComponent(ComponentIds.Hero); }
            set {
                if (value != isHero) {
                    if (value) {
                        AddComponent(ComponentIds.Hero, heroComponent);
                    } else {
                        RemoveComponent(ComponentIds.Hero);
                    }
                }
            }
        }

        public Entity IsHero(bool value) {
            isHero = value;
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherHero;

        public static IMatcher Hero {
            get {
                if (_matcherHero == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Hero);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherHero = matcher;
                }

                return _matcherHero;
            }
        }
    }
}
