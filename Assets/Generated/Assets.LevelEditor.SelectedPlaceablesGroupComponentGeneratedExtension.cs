using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.LevelEditor.SelectedPlaceablesGroupComponent selectedPlaceablesGroup { get { return (Assets.LevelEditor.SelectedPlaceablesGroupComponent)GetComponent(GameComponentIds.SelectedPlaceablesGroup); } }

        public bool hasSelectedPlaceablesGroup { get { return HasComponent(GameComponentIds.SelectedPlaceablesGroup); } }

        public Entity AddSelectedPlaceablesGroup(Assets.LevelEditor.SelectionGroup newGroup) {
            var componentPool = GetComponentPool(GameComponentIds.SelectedPlaceablesGroup);
            var component = (Assets.LevelEditor.SelectedPlaceablesGroupComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.LevelEditor.SelectedPlaceablesGroupComponent());
            component.Group = newGroup;
            return AddComponent(GameComponentIds.SelectedPlaceablesGroup, component);
        }

        public Entity ReplaceSelectedPlaceablesGroup(Assets.LevelEditor.SelectionGroup newGroup) {
            var componentPool = GetComponentPool(GameComponentIds.SelectedPlaceablesGroup);
            var component = (Assets.LevelEditor.SelectedPlaceablesGroupComponent)(componentPool.Count > 0 ? componentPool.Pop() : new Assets.LevelEditor.SelectedPlaceablesGroupComponent());
            component.Group = newGroup;
            ReplaceComponent(GameComponentIds.SelectedPlaceablesGroup, component);
            return this;
        }

        public Entity RemoveSelectedPlaceablesGroup() {
            return RemoveComponent(GameComponentIds.SelectedPlaceablesGroup);;
        }
    }

    public partial class Pool {
        public Entity selectedPlaceablesGroupEntity { get { return GetGroup(GameMatcher.SelectedPlaceablesGroup).GetSingleEntity(); } }

        public Assets.LevelEditor.SelectedPlaceablesGroupComponent selectedPlaceablesGroup { get { return selectedPlaceablesGroupEntity.selectedPlaceablesGroup; } }

        public bool hasSelectedPlaceablesGroup { get { return selectedPlaceablesGroupEntity != null; } }

        public Entity SetSelectedPlaceablesGroup(Assets.LevelEditor.SelectionGroup newGroup) {
            if (hasSelectedPlaceablesGroup) {
                throw new EntitasException("Could not set selectedPlaceablesGroup!\n" + this + " already has an entity with Assets.LevelEditor.SelectedPlaceablesGroupComponent!",
                    "You should check if the pool already has a selectedPlaceablesGroupEntity before setting it or use pool.ReplaceSelectedPlaceablesGroup().");
            }
            var entity = CreateEntity();
            entity.AddSelectedPlaceablesGroup(newGroup);
            return entity;
        }

        public Entity ReplaceSelectedPlaceablesGroup(Assets.LevelEditor.SelectionGroup newGroup) {
            var entity = selectedPlaceablesGroupEntity;
            if (entity == null) {
                entity = SetSelectedPlaceablesGroup(newGroup);
            } else {
                entity.ReplaceSelectedPlaceablesGroup(newGroup);
            }

            return entity;
        }

        public void RemoveSelectedPlaceablesGroup() {
            DestroyEntity(selectedPlaceablesGroupEntity);
        }
    }
}

    public partial class GameMatcher {
        static IMatcher _matcherSelectedPlaceablesGroup;

        public static IMatcher SelectedPlaceablesGroup {
            get {
                if (_matcherSelectedPlaceablesGroup == null) {
                    var matcher = (Matcher)Matcher.AllOf(GameComponentIds.SelectedPlaceablesGroup);
                    matcher.componentNames = GameComponentIds.componentNames;
                    _matcherSelectedPlaceablesGroup = matcher;
                }

                return _matcherSelectedPlaceablesGroup;
            }
        }
    }
