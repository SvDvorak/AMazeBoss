using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.SceneComponent scene { get { return (Assets.SceneComponent)GetComponent(GameComponentIds.Scene); } }

        public bool hasScene { get { return HasComponent(GameComponentIds.Scene); } }

        public Entity AddScene(Assets.Scene newValue) {
            var componentPool = GetComponentPool(GameComponentIds.Scene);
            var component = (Assets.SceneComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.SceneComponent());
            component.Value = newValue;
            return AddComponent(GameComponentIds.Scene, component);
        }

        public Entity ReplaceScene(Assets.Scene newValue) {
            var componentPool = GetComponentPool(GameComponentIds.Scene);
            var component = (Assets.SceneComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.SceneComponent());
            component.Value = newValue;
            ReplaceComponent(GameComponentIds.Scene, component);
            return this;
        }

        public Entity RemoveScene() {
            return RemoveComponent(GameComponentIds.Scene);;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherScene;

        public static IMatcher Scene {
            get {
                if (_matcherScene == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.Scene);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherScene = matcher;
                }

                return _matcherScene;
            }
        }
    }

    public partial class UiMatcher {
        static IMatcher _matcherScene;

        public static IMatcher Scene {
            get {
                if (_matcherScene == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.Scene);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherScene = matcher;
                }

                return _matcherScene;
            }
        }
    }
