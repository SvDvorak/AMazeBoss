using System.Collections.Generic;
using System.Linq;
using Assets.LevelEditor.Placeables;
using Entitas;

namespace Assets.LevelEditor
{
    public class SelectionGroup
    {
        private readonly List<IPlaceable> _placeables;
        private int _selectedIndex;

        public SelectionGroup(params IPlaceable[] placeables)
        {
            _placeables = placeables.ToList();
        }

        public IPlaceable GetCurrentSelection()
        {
            return _placeables[_selectedIndex];
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
                    { 1, new SelectionGroup(AllPlaceables.Empty) },
                    { 2, new SelectionGroup(AllPlaceables.Pillar, AllPlaceables.Wall) },
                    { 3, new SelectionGroup(AllPlaceables.SpikeTrap, AllPlaceables.WallTrap, AllPlaceables.CurseTrigger) },
                    { 4, new SelectionGroup(AllPlaceables.Spikes, AllPlaceables.Box) },
                    { 5, new SelectionGroup(AllPlaceables.Hero, AllPlaceables.Boss) },
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

            input.ReplaceSelectedPlaceable(selectedGroup.GetCurrentSelection());
        }
    }
}
