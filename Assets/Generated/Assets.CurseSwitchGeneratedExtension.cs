using Entitas;

namespace Entitas {
    public partial class Entity {
        static readonly Assets.CurseSwitch curseSwitchComponent = new Assets.CurseSwitch();

        public bool isCurseSwitch {
            get { return HasComponent(GameComponentIds.CurseSwitch); }
            set {
                if (value != isCurseSwitch) {
                    if (value) {
                        AddComponent(GameComponentIds.CurseSwitch, curseSwitchComponent);
                    } else {
                        RemoveComponent(GameComponentIds.CurseSwitch);
                    }
                }
            }
        }

        public Entity IsCurseSwitch(bool value) {
            isCurseSwitch = value;
            return this;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherCurseSwitch;

        public static IMatcher CurseSwitch {
            get {
                if (_matcherCurseSwitch == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.CurseSwitch);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherCurseSwitch = matcher;
                }

                return _matcherCurseSwitch;
            }
        }
    }
