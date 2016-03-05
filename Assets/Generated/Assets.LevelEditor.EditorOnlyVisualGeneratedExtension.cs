using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.LevelEditor.EditorOnlyVisual editorOnlyVisual { get { return (Assets.LevelEditor.EditorOnlyVisual)GetComponent(GameComponentIds.EditorOnlyVisual); } }

        public bool hasEditorOnlyVisual { get { return HasComponent(GameComponentIds.EditorOnlyVisual); } }

        public Entity AddEditorOnlyVisual(Assets.LevelEditor.ViewMode newShowInMode) {
            var componentPool = GetComponentPool(GameComponentIds.EditorOnlyVisual);
            var component = (Assets.LevelEditor.EditorOnlyVisual)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.LevelEditor.EditorOnlyVisual());
            component.ShowInMode = newShowInMode;
            return AddComponent(GameComponentIds.EditorOnlyVisual, component);
        }

        public Entity ReplaceEditorOnlyVisual(Assets.LevelEditor.ViewMode newShowInMode) {
            var componentPool = GetComponentPool(GameComponentIds.EditorOnlyVisual);
            var component = (Assets.LevelEditor.EditorOnlyVisual)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.LevelEditor.EditorOnlyVisual());
            component.ShowInMode = newShowInMode;
            ReplaceComponent(GameComponentIds.EditorOnlyVisual, component);
            return this;
        }

        public Entity RemoveEditorOnlyVisual() {
            return RemoveComponent(GameComponentIds.EditorOnlyVisual);;
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherEditorOnlyVisual;

        public static IMatcher EditorOnlyVisual {
            get {
                if (_matcherEditorOnlyVisual == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.EditorOnlyVisual);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherEditorOnlyVisual = matcher;
                }

                return _matcherEditorOnlyVisual;
            }
        }
    }

    public partial class UiMatcher {
        static IMatcher _matcherEditorOnlyVisual;

        public static IMatcher EditorOnlyVisual {
            get {
                if (_matcherEditorOnlyVisual == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.EditorOnlyVisual);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherEditorOnlyVisual = matcher;
                }

                return _matcherEditorOnlyVisual;
            }
        }
    }
