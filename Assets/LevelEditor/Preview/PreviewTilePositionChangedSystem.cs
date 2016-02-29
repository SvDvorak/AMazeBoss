using System.Collections.Generic;
using Entitas;

namespace Assets.LevelEditor.Preview
{
    public class PreviewTilePositionChangedSystem : PreviewChangedSystem, IReactiveSystem
    {
        public TriggerOnEvent trigger { get { return GameMatcher.Position.OnEntityAdded(); } }

        public void Execute(List<Entity> entities)
        {
            var tilePosition = entities.SingleEntity().position.Value;

            GetPreviewEntity().ReplacePosition(tilePosition);
        }
    }
}