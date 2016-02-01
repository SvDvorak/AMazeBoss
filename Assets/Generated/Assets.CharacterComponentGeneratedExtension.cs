namespace Entitas {
    public partial class Entity {
        static readonly Assets.CharacterComponent characterComponent = new Assets.CharacterComponent();

        public bool isCharacter {
            get { return HasComponent(ComponentIds.Character); }
            set {
                if (value != isCharacter) {
                    if (value) {
                        AddComponent(ComponentIds.Character, characterComponent);
                    } else {
                        RemoveComponent(ComponentIds.Character);
                    }
                }
            }
        }

        public Entity IsCharacter(bool value) {
            isCharacter = value;
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherCharacter;

        public static IMatcher Character {
            get {
                if (_matcherCharacter == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Character);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherCharacter = matcher;
                }

                return _matcherCharacter;
            }
        }
    }
}
