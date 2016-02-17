using Entitas;

namespace Entitas {
    public partial class Entity {
        static readonly Assets.Input.InputCurseSwitchComponent inputCurseSwitchComponent = new Assets.Input.InputCurseSwitchComponent();

        public bool isInputCurseSwitch {
            get { return HasComponent(GameComponentIds.InputCurseSwitch); }
            set {
                if (value != isInputCurseSwitch) {
                    if (value) {
                        AddComponent(GameComponentIds.InputCurseSwitch, inputCurseSwitchComponent);
                    } else {
                        RemoveComponent(GameComponentIds.InputCurseSwitch);
                    }
                }
            }
        }

        public Entity IsInputCurseSwitch(bool value) {
            isInputCurseSwitch = value;
            return this;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherInputCurseSwitch;

        public static IMatcher InputCurseSwitch {
            get {
                if (_matcherInputCurseSwitch == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.InputCurseSwitch);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherInputCurseSwitch = matcher;
                }

                return _matcherInputCurseSwitch;
            }
        }
    }
