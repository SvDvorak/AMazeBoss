namespace Entitas {
    public partial class Entity {
        static readonly Assets.Input.InputItemInteractComponent inputItemInteractComponent = new Assets.Input.InputItemInteractComponent();

        public bool isInputItemInteract {
            get { return HasComponent(ComponentIds.InputItemInteract); }
            set {
                if (value != isInputItemInteract) {
                    if (value) {
                        AddComponent(ComponentIds.InputItemInteract, inputItemInteractComponent);
                    } else {
                        RemoveComponent(ComponentIds.InputItemInteract);
                    }
                }
            }
        }

        public Entity IsInputItemInteract(bool value) {
            isInputItemInteract = value;
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherInputItemInteract;

        public static IMatcher InputItemInteract {
            get {
                if (_matcherInputItemInteract == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.InputItemInteract);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherInputItemInteract = matcher;
                }

                return _matcherInputItemInteract;
            }
        }
    }
}
