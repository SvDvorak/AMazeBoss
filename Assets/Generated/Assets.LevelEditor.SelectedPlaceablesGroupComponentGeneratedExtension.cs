using System.Collections.Generic;

using Entitas;

namespace Entitas {
    public partial class Entity {
        public Assets.LevelEditor.SelectedPlaceablesGroupComponent selectedPlaceablesGroup { get { return (Assets.LevelEditor.SelectedPlaceablesGroupComponent)GetComponent(GameComponentIds.SelectedPlaceablesGroup); } }

        public bool hasSelectedPlaceablesGroup { get { return HasComponent(GameComponentIds.SelectedPlaceablesGroup); } }

        static readonly Stack<Assets.LevelEditor.SelectedPlaceablesGroupComponent> _selectedPlaceablesGroupComponentPool = new Stack<Assets.LevelEditor.SelectedPlaceablesGroupComponent>();

        public static void ClearSelectedPlaceablesGroupComponentPool() {
            _selectedPlaceablesGroupComponentPool.Clear();
        }

        public Entity AddSelectedPlaceablesGroup(Assets.LevelEditor.SelectionGroup newGroup) {
            var component = _selectedPlaceablesGroupComponentPool.Count > 0 ? _selectedPlaceablesGroupComponentPool.Pop() : new Assets.LevelEditor.SelectedPlaceablesGroupComponent();
            component.Group = newGroup;
            return AddComponent(GameComponentIds.SelectedPlaceablesGroup, component);
        }

        public Entity ReplaceSelectedPlaceablesGroup(Assets.LevelEditor.SelectionGroup newGroup) {
            var previousComponent = hasSelectedPlaceablesGroup ? selectedPlaceablesGroup : null;
            var component = _selectedPlaceablesGroupComponentPool.Count > 0 ? _selectedPlaceablesGroupComponentPool.Pop() : new Assets.LevelEditor.SelectedPlaceablesGroupComponent();
            component.Group = newGroup;
            ReplaceComponent(GameComponentIds.SelectedPlaceablesGroup, component);
            if (previousComponent != null) {
                _selectedPlaceablesGroupComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveSelectedPlaceablesGroup() {
            var component = selectedPlaceablesGroup;
            RemoveComponent(GameComponentIds.SelectedPlaceablesGroup);
            _selectedPlaceablesGroupComponentPool.Push(component);
            return this;
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
