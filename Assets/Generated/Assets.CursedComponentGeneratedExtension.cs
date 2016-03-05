using Entitas;

namespace Entitas {
    public partial class Entity {
        static readonly Assets.CursedComponent cursedComponent = new Assets.CursedComponent();

        public bool isCursed {
            get { return HasComponent(GameComponentIds.Cursed); }
            set {
                if (value != isCursed) {
                    if (value) {
                        AddComponent(GameComponentIds.Cursed, cursedComponent);
                    } else {
                        RemoveComponent(GameComponentIds.Cursed);
                    }
                }
            }
        }

        public Entity IsCursed(bool value) {
            isCursed = value;
            return this;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherCursed;

        public static IMatcher Cursed {
            get {
                if (_matcherCursed == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.Cursed);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherCursed = matcher;
                }

                return _matcherCursed;
            }
        }
    }
