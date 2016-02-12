using System.Collections.Generic;

using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.MainMenu.TextComponent text { get { return (Assets.MainMenu.TextComponent)GetComponent(UiComponentIds.Text); } }

        public bool hasText { get { return HasComponent(UiComponentIds.Text); } }

        static readonly Stack<Assets.MainMenu.TextComponent> _textComponentPool = new Stack<Assets.MainMenu.TextComponent>();

        public static void ClearTextComponentPool() {
            _textComponentPool.Clear();
        }

        public Entity AddText(UnityEngine.UI.Text newText) {
            var component = _textComponentPool.Count > 0 ? _textComponentPool.Pop() : new Assets.MainMenu.TextComponent();
            component.Text = newText;
            return AddComponent(UiComponentIds.Text, component);
        }

        public Entity ReplaceText(UnityEngine.UI.Text newText) {
            var previousComponent = hasText ? text : null;
            var component = _textComponentPool.Count > 0 ? _textComponentPool.Pop() : new Assets.MainMenu.TextComponent();
            component.Text = newText;
            ReplaceComponent(UiComponentIds.Text, component);
            if (previousComponent != null) {
                _textComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveText() {
            var component = text;
            RemoveComponent(UiComponentIds.Text);
            _textComponentPool.Push(component);
            return this;
        }
    }
}

    public partial class UiMatcher {
        static IMatcher _matcherText;

        public static IMatcher Text {
            get {
                if (_matcherText == null) {
                    var matcher = (Matcher)Matcher.AllOf(UiComponentIds.Text);
                    matcher.componentNames = UiComponentIds.componentNames;
                    _matcherText = matcher;
                }

                return _matcherText;
            }
        }
    }
