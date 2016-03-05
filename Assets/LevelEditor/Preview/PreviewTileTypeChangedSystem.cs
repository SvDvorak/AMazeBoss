using System.Collections.Generic;
using Entitas;

namespace Assets.LevelEditor.Preview
{
    public class PreviewTileTypeChangedSystem : PreviewChangedSystem, IReactiveSystem
    {
        public TriggerOnEvent trigger { get { return GameMatcher.SelectedPlaceable.OnEntityAdded(); } }

        public void Execute(List<Entity> entities)
        {
            var selectedPlaceable = entities.SingleEntity().selectedPlaceable.Value;

            var oldPreview = GetPreviewEntity();
            oldPreview.IsDestroyed(true);

            selectedPlaceable.Do(Pool.CreateEntity()).IsPreview(true).AddPosition(oldPreview.position.Value);
        }
    }
}
