using System.Collections.Generic;
using System.Linq;
using Entitas;

namespace Assets.EntitasRefactor.Preview
{
    public class PreviewTileTypeChangedSystem : PreviewChangedSystem, IReactiveSystem
    {
        public TriggerOnEvent trigger { get { return Matcher.TileSelect.OnEntityAdded(); } }

        public void Execute(List<Entity> entities)
        {
            var selectedTile = entities.SingleEntity().tileSelect.Type;

            var tileTemplates = Pool.tileTemplates.Value.Retrieve(selectedTile);
            Pool.previewEntity
                .ReplaceResource(tileTemplates.Item2.First());
        }
    }
}
