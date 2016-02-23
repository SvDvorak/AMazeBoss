using Entitas;

namespace Entitas {
    public partial class Entity {
        static readonly Assets.LevelEditor.EditorOnlyVisual editorOnlyVisualComponent = new Assets.LevelEditor.EditorOnlyVisual();

        public bool isEditorOnlyVisual {
            get { return HasComponent(GameComponentIds.EditorOnlyVisual); }
            set {
                if (value != isEditorOnlyVisual) {
                    if (value) {
                        AddComponent(GameComponentIds.EditorOnlyVisual, editorOnlyVisualComponent);
                    } else {
                        RemoveComponent(GameComponentIds.EditorOnlyVisual);
                    }
                }
            }
        }

        public Entity IsEditorOnlyVisual(bool value) {
            isEditorOnlyVisual = value;
            return this;
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
