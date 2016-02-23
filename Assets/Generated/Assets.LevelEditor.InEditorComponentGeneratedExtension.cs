using Entitas;

namespace Entitas {
    public partial class Entity {
        static readonly Assets.LevelEditor.InEditorComponent inEditorComponent = new Assets.LevelEditor.InEditorComponent();

        public bool isInEditor {
            get { return HasComponent(GameComponentIds.InEditor); }
            set {
                if (value != isInEditor) {
                    if (value) {
                        AddComponent(GameComponentIds.InEditor, inEditorComponent);
                    } else {
                        RemoveComponent(GameComponentIds.InEditor);
                    }
                }
            }
        }

        public Entity IsInEditor(bool value) {
            isInEditor = value;
            return this;
        }
    }

    public partial class Pool {
        public Entity inEditorEntity { get { return GetGroup(GameMatcher.InEditor).GetSingleEntity(); } }

        public bool isInEditor {
            get { return inEditorEntity != null; }
            set {
                var entity = inEditorEntity;
                if (value != (entity != null)) {
                    if (value) {
                        CreateEntity().isInEditor = true;
                    } else {
                        DestroyEntity(entity);
                    }
                }
            }
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherInEditor;

        public static IMatcher InEditor {
            get {
                if (_matcherInEditor == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.InEditor);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherInEditor = matcher;
                }

                return _matcherInEditor;
            }
        }
    }
