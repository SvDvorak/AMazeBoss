using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.ViewComponent view { get { return (Assets.ViewComponent)GetComponent(GameComponentIds.View); } }

        public bool hasView { get { return HasComponent(GameComponentIds.View); } }

        public Entity AddView(UnityEngine.GameObject newValue) {
            var componentPool = GetComponentPool(GameComponentIds.View);
            var component = (Assets.ViewComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.ViewComponent());
            component.Value = newValue;
            return AddComponent(GameComponentIds.View, component);
        }

        public Entity ReplaceView(UnityEngine.GameObject newValue) {
            var componentPool = GetComponentPool(GameComponentIds.View);
            var component = (Assets.ViewComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.ViewComponent());
            component.Value = newValue;
            ReplaceComponent(GameComponentIds.View, component);
            return this;
        }

        public Entity RemoveView() {
            return RemoveComponent(GameComponentIds.View);;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherView;

        public static IMatcher View {
            get {
                if (_matcherView == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.View);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherView = matcher;
                }

                return _matcherView;
            }
        }
    }

    public partial class UiMatcher {
        static IMatcher _matcherView;

        public static IMatcher View {
            get {
                if (_matcherView == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.View);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherView = matcher;
                }

                return _matcherView;
            }
        }
    }
