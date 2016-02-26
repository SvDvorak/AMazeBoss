using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.LevelEditor.EditorViewMode editorViewMode { get { return (Assets.LevelEditor.EditorViewMode)GetComponent(GameComponentIds.EditorViewMode); } }

        public bool hasEditorViewMode { get { return HasComponent(GameComponentIds.EditorViewMode); } }

        public Entity AddEditorViewMode(Assets.LevelEditor.ViewMode newValue) {
            var componentPool = GetComponentPool(GameComponentIds.EditorViewMode);
            var component = (Assets.LevelEditor.EditorViewMode)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.LevelEditor.EditorViewMode());
            component.Value = newValue;
            return AddComponent(GameComponentIds.EditorViewMode, component);
        }

        public Entity ReplaceEditorViewMode(Assets.LevelEditor.ViewMode newValue) {
            var componentPool = GetComponentPool(GameComponentIds.EditorViewMode);
            var component = (Assets.LevelEditor.EditorViewMode)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.LevelEditor.EditorViewMode());
            component.Value = newValue;
            ReplaceComponent(GameComponentIds.EditorViewMode, component);
            return this;
        }

        public Entity RemoveEditorViewMode() {
            return RemoveComponent(GameComponentIds.EditorViewMode);;
        }
    }

    public partial class Pool {
        public Entity editorViewModeEntity { get { return GetGroup(GameMatcher.EditorViewMode).GetSingleEntity(); } }

        public Assets.LevelEditor.EditorViewMode editorViewMode { get { return editorViewModeEntity.editorViewMode; } }

        public bool hasEditorViewMode { get { return editorViewModeEntity != null; } }

        public Entity SetEditorViewMode(Assets.LevelEditor.ViewMode newValue) {
            if (hasEditorViewMode) {
                throw new EntitasException("Could not set editorViewMode!\n" + this + " already has an entity with Assets.LevelEditor.EditorViewMode!",
                    "You should check if the pool already has a editorViewModeEntity before setting it or use pool.ReplaceEditorViewMode().");
            }
            var entity = CreateEntity();
            entity.AddEditorViewMode(newValue);
            return entity;
        }

        public Entity ReplaceEditorViewMode(Assets.LevelEditor.ViewMode newValue) {
            var entity = editorViewModeEntity;
            if (entity == null) {
                entity = SetEditorViewMode(newValue);
            } else {
                entity.ReplaceEditorViewMode(newValue);
            }

            return entity;
        }

        public void RemoveEditorViewMode() {
            DestroyEntity(editorViewModeEntity);
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherEditorViewMode;

        public static IMatcher EditorViewMode {
            get {
                if (_matcherEditorViewMode == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.EditorViewMode);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherEditorViewMode = matcher;
                }

                return _matcherEditorViewMode;
            }
        }
    }
