using Entitas;

namespace Entitas {
    public partial class Entity {
        static readonly Assets.AreaComponent areaComponent = new Assets.AreaComponent();

        public bool isArea {
            get { return HasComponent(GameComponentIds.Area); }
            set {
                if (value != isArea) {
                    if (value) {
                        AddComponent(GameComponentIds.Area, areaComponent);
                    } else {
                        RemoveComponent(GameComponentIds.Area);
                    }
                }
            }
        }

        public Entity IsArea(bool value) {
            isArea = value;
            return this;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherArea;

        public static IMatcher Area {
            get {
                if (_matcherArea == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.Area);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherArea = matcher;
                }

                return _matcherArea;
            }
        }
    }
