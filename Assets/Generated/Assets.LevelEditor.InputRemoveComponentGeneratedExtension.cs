using Entitas;

namespace Entitas {
    public partial class Entity {
        static readonly Assets.LevelEditor.InputRemoveComponent inputRemoveComponent = new Assets.LevelEditor.InputRemoveComponent();

        public bool isInputRemove {
            get { return HasComponent(GameComponentIds.InputRemove); }
            set {
                if (value != isInputRemove) {
                    if (value) {
                        AddComponent(GameComponentIds.InputRemove, inputRemoveComponent);
                    } else {
                        RemoveComponent(GameComponentIds.InputRemove);
                    }
                }
            }
        }

        public Entity IsInputRemove(bool value) {
            isInputRemove = value;
            return this;
        }
    }

    public partial class Pool {
        public Entity inputRemoveEntity { get { return GetGroup(GameMatcher.InputRemove).GetSingleEntity(); } }

        public bool isInputRemove {
            get { return inputRemoveEntity != null; }
            set {
                var entity = inputRemoveEntity;
                if (value != (entity != null)) {
                    if (value) {
                        CreateEntity().isInputRemove = true;
                    } else {
                        DestroyEntity(entity);
                    }
                }
            }
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherInputRemove;

        public static IMatcher InputRemove {
            get {
                if (_matcherInputRemove == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.InputRemove);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherInputRemove = matcher;
                }

                return _matcherInputRemove;
            }
        }
    }
