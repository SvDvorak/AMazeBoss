using Entitas;

namespace Entitas {
    public partial class Entity {
        static readonly Assets.WallComponent wallComponent = new Assets.WallComponent();

        public bool isWall {
            get { return HasComponent(GameComponentIds.Wall); }
            set {
                if (value != isWall) {
                    if (value) {
                        AddComponent(GameComponentIds.Wall, wallComponent);
                    } else {
                        RemoveComponent(GameComponentIds.Wall);
                    }
                }
            }
        }

        public Entity IsWall(bool value) {
            isWall = value;
            return this;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherWall;

        public static IMatcher Wall {
            get {
                if (_matcherWall == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.Wall);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherWall = matcher;
                }

                return _matcherWall;
            }
        }
    }
