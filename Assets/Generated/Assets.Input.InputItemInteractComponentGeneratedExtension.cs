using Entitas;

namespace Entitas {
    public partial class Entity {
        static readonly Assets.Input.InputItemInteractComponent inputItemInteractComponent = new Assets.Input.InputItemInteractComponent();

        public bool isInputItemInteract {
            get { return HasComponent(GameComponentIds.InputItemInteract); }
            set {
                if (value != isInputItemInteract) {
                    if (value) {
                        AddComponent(GameComponentIds.InputItemInteract, inputItemInteractComponent);
                    } else {
                        RemoveComponent(GameComponentIds.InputItemInteract);
                    }
                }
            }
        }

        public Entity IsInputItemInteract(bool value) {
            isInputItemInteract = value;
            return this;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherInputItemInteract;

        public static IMatcher InputItemInteract {
            get {
                if (_matcherInputItemInteract == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.InputItemInteract);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherInputItemInteract = matcher;
                }

                return _matcherInputItemInteract;
            }
        }
    }
