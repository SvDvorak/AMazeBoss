using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.MainMenu.TextComponent text { get { return (Assets.MainMenu.TextComponent)GetComponent(UiComponentIds.Text); } }

        public bool hasText { get { return HasComponent(UiComponentIds.Text); } }

        public Entity AddText(UnityEngine.UI.Text newText) {
            var componentPool = GetComponentPool(UiComponentIds.Text);
            var component = (Assets.MainMenu.TextComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.MainMenu.TextComponent());
            component.Text = newText;
            return AddComponent(UiComponentIds.Text, component);
        }

        public Entity ReplaceText(UnityEngine.UI.Text newText) {
            var componentPool = GetComponentPool(UiComponentIds.Text);
            var component = (Assets.MainMenu.TextComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.MainMenu.TextComponent());
            component.Text = newText;
            ReplaceComponent(UiComponentIds.Text, component);
            return this;
        }

        public Entity RemoveText() {
            return RemoveComponent(UiComponentIds.Text);;
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
