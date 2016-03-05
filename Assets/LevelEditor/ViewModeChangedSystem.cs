using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

namespace Assets.LevelEditor
{
    public class ViewModeChangedSystem : IReactiveSystem, ISetPool
    {
        private Group _editorVisualsGroup;

        public TriggerOnEvent trigger { get { return GameMatcher.EditorViewMode.OnEntityAdded(); } }

        public void SetPool(Pool pool)
        {
            _editorVisualsGroup = pool.GetGroup(Matcher.AllOf(GameMatcher.EditorOnlyVisual, GameMatcher.View));
        }

        public void Execute(List<Entity> entities)
        {
            var currentViewMode = entities.SingleEntity().editorViewMode.Value;
            _editorVisualsGroup.GetEntities().ForEach(x => ViewModeState.UpdateVisual(currentViewMode, x));
        }
    }

    public class ViewModeVisualAddedSystem : IReactiveSystem, ISetPool
    {
        private Pool _pool;

        public TriggerOnEvent trigger { get { return Matcher.AllOf(GameMatcher.EditorOnlyVisual, GameMatcher.View).OnEntityAdded(); } }

        public void SetPool(Pool pool)
        {
            _pool = pool;
        }

        public void Execute(List<Entity> entities)
        {
            var currentViewMode = _pool.editorViewMode.Value;
            entities.ForEach(x => ViewModeState.UpdateVisual(currentViewMode, x));
        }
    }

    public static class ViewModeState
    {
        public static void UpdateVisual(ViewMode currentViewMode, Entity entity)
        {
            var entityShowMode = entity.editorOnlyVisual.ShowInMode;
            entity.view.Value
                .GetComponentsInChildren<Renderer>()
                .ToList()
                .ForEach(x => x.enabled = currentViewMode == entityShowMode);
        }
    }
}