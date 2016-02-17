using Entitas;

namespace Entitas {
    public partial class Entity {
        static readonly Assets.CharacterComponent characterComponent = new Assets.CharacterComponent();

        public bool isCharacter {
            get { return HasComponent(GameComponentIds.Character); }
            set {
                if (value != isCharacter) {
                    if (value) {
                        AddComponent(GameComponentIds.Character, characterComponent);
                    } else {
                        RemoveComponent(GameComponentIds.Character);
                    }
                }
            }
        }

        public Entity IsCharacter(bool value) {
            isCharacter = value;
            return this;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherCharacter;

        public static IMatcher Character {
            get {
                if (_matcherCharacter == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.Character);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherCharacter = matcher;
                }

                return _matcherCharacter;
            }
        }
    }
