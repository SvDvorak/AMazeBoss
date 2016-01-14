namespace Entitas {
    public partial class Entity {
        static readonly Assets.CurseSwitch curseSwitchComponent = new Assets.CurseSwitch();

        public bool isCurseSwitch {
            get { return HasComponent(ComponentIds.CurseSwitch); }
            set {
                if (value != isCurseSwitch) {
                    if (value) {
                        AddComponent(ComponentIds.CurseSwitch, curseSwitchComponent);
                    } else {
                        RemoveComponent(ComponentIds.CurseSwitch);
                    }
                }
            }
        }

        public Entity IsCurseSwitch(bool value) {
            isCurseSwitch = value;
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherCurseSwitch;

        public static IMatcher CurseSwitch {
            get {
                if (_matcherCurseSwitch == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.CurseSwitch);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherCurseSwitch = matcher;
                }

                return _matcherCurseSwitch;
            }
        }
    }
}
