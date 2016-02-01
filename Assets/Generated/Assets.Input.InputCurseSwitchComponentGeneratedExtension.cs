namespace Entitas {
    public partial class Entity {
        static readonly Assets.Input.InputCurseSwitchComponent inputCurseSwitchComponent = new Assets.Input.InputCurseSwitchComponent();

        public bool isInputCurseSwitch {
            get { return HasComponent(ComponentIds.InputCurseSwitch); }
            set {
                if (value != isInputCurseSwitch) {
                    if (value) {
                        AddComponent(ComponentIds.InputCurseSwitch, inputCurseSwitchComponent);
                    } else {
                        RemoveComponent(ComponentIds.InputCurseSwitch);
                    }
                }
            }
        }

        public Entity IsInputCurseSwitch(bool value) {
            isInputCurseSwitch = value;
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherInputCurseSwitch;

        public static IMatcher InputCurseSwitch {
            get {
                if (_matcherInputCurseSwitch == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.InputCurseSwitch);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherInputCurseSwitch = matcher;
                }

                return _matcherInputCurseSwitch;
            }
        }
    }
}
