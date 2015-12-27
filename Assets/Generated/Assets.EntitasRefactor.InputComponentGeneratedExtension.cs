namespace Entitas {
    public partial class Entity {
        static readonly Assets.LevelEditor.InputComponent inputComponent = new Assets.LevelEditor.InputComponent();

        public bool isInput {
            get { return HasComponent(ComponentIds.Input); }
            set {
                if (value != isInput) {
                    if (value) {
                        AddComponent(ComponentIds.Input, inputComponent);
                    } else {
                        RemoveComponent(ComponentIds.Input);
                    }
                }
            }
        }

        public Entity IsInput(bool value) {
            isInput = value;
            return this;
        }
    }

    public partial class Pool {
        public Entity inputEntity { get { return GetGroup(Matcher.Input).GetSingleEntity(); } }

        public bool isInput {
            get { return inputEntity != null; }
            set {
                var entity = inputEntity;
                if (value != (entity != null)) {
                    if (value) {
                        CreateEntity().isInput = true;
                    } else {
                        DestroyEntity(entity);
                    }
                }
            }
        }
    }

    public partial class Matcher {
        static IMatcher _matcherInput;

        public static IMatcher Input {
            get {
                if (_matcherInput == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Input);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherInput = matcher;
                }

                return _matcherInput;
            }
        }
    }
}
