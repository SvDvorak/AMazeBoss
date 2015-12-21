using System.Collections.Generic;
using Entitas;

namespace Assets.EntitasRefactor.Preview
{
    public class PreviewTileTypeChangedSystem : PreviewChangedSystem, IReactiveSystem
    {
        public TriggerOnEvent trigger { get { return Matcher.TileSelect.OnEntityAdded(); } }

        public void Execute(List<Entity> entities)
        {
            var selectedTile = entities.SingleEntity().tileSelect.Type;

            GetPreviewEntity()
                .ReplaceTile(selectedTile)
                .ReplaceResource("Tiles/Normal");
        }
    }
}
