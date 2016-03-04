using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;

namespace Assets.LevelEditor
{
    public class SelectionGroup
    {
        private readonly ViewMode _viewMode;
        private readonly List<EntityPerformer> _placeables;
        private int _selectedIndex;

        public SelectionGroup(ViewMode viewMode, params EntityPerformer[] placeables)
        {
            _viewMode = viewMode;
            _placeables = placeables.ToList();
        }

        public SelectionGroup(params EntityPerformer[] placeables) : this(ViewMode.Normal, placeables)
        {
        }

        public EntityPerformer GetCurrentSelection()
        {
            return _placeables[_selectedIndex];
        }

        public ViewMode GetViewMode()
        {
            return _viewMode;
        }

        public void RotateSelection()
        {
            _selectedIndex = (_selectedIndex + 1) % _placeables.Count;
        }
    }

    public class SelectPlaceableSystem : IInitializeSystem, IExecuteSystem, ISetPool
    {
        private readonly Dictionary<int, SelectionGroup> _numberToPlaceable = new Dictionary<int, SelectionGroup>
                {
                    { 1, new SelectionGroup(WorldObjects.Empty) },
                    { 2, new SelectionGroup(WorldObjects.Pillar, WorldObjects.Wall) },
                    { 3, new SelectionGroup(WorldObjects.SpikeTrap, WorldObjects.WallTrap, WorldObjects.CurseTrigger, WorldObjects.ExitGate) },
                    { 4, new SelectionGroup(WorldObjects.Spikes, WorldObjects.Box) },
                    { 5, new SelectionGroup(WorldObjects.Hero, WorldObjects.Boss) },
                    { 6, new SelectionGroup(ViewMode.Area, WorldObjects.ExitTrigger, WorldObjects.PuzzleArea) }
                };

        private Pool _pool;

        public void SetPool(Pool pool)
        {
            _pool = pool;
        }

        public void Initialize()
        {
            SetPlaceableSelected(_numberToPlaceable.First().Value);
        }

        public void Execute()
        {
            if (_pool.isPaused)
            {
                return;
            }

            SelectionGroup selectedGroup = null;
            for (int i = 1; i <= _numberToPlaceable.Keys.Count; i++)
            {
                if (UnityEngine.Input.GetKeyDown(i.ToString()))
                {
                    selectedGroup = _numberToPlaceable[i];
                }
            }

            SetPlaceableSelected(selectedGroup);
        }

        private void SetPlaceableSelected(SelectionGroup selectedGroup)
        {
            if (selectedGroup == null)
            {
                return;
            }

            var input = _pool.inputEntity;
            if (input.hasSelectedPlaceablesGroup && input.selectedPlaceablesGroup.Group == selectedGroup)
            {
                selectedGroup.RotateSelection();
            }
            else
            {
                input.ReplaceSelectedPlaceablesGroup(selectedGroup);
            }

            _pool.ReplaceEditorViewMode(selectedGroup.GetViewMode());
            input.ReplaceSelectedPlaceable(selectedGroup.GetCurrentSelection());
        }
    }
}
