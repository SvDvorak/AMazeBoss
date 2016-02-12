using System.Collections.Generic;

using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.ViewComponent view { get { return (Assets.ViewComponent)GetComponent(GameComponentIds.View); } }

        public bool hasView { get { return HasComponent(GameComponentIds.View); } }

        static readonly Stack<Assets.ViewComponent> _viewComponentPool = new Stack<Assets.ViewComponent>();

        public static void ClearViewComponentPool() {
            _viewComponentPool.Clear();
        }

        public Entity AddView(UnityEngine.GameObject newValue) {
            var component = _viewComponentPool.Count > 0 ? _viewComponentPool.Pop() : new Assets.ViewComponent();
            component.Value = newValue;
            return AddComponent(GameComponentIds.View, component);
        }

        public Entity ReplaceView(UnityEngine.GameObject newValue) {
            var previousComponent = hasView ? view : null;
            var component = _viewComponentPool.Count > 0 ? _viewComponentPool.Pop() : new Assets.ViewComponent();
            component.Value = newValue;
            ReplaceComponent(GameComponentIds.View, component);
            if (previousComponent != null) {
                _viewComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveView() {
            var component = view;
            RemoveComponent(GameComponentIds.View);
            _viewComponentPool.Push(component);
            return this;
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
