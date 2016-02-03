using System.Collections.Generic;
using System.Linq;
using Entitas;

namespace Assets.LevelEditor.Preview
{
    public class PreviewTileTypeChangedSystem : PreviewChangedSystem, IReactiveSystem
    {
        public TriggerOnEvent trigger { get { return Matcher.SelectedPlaceable.OnEntityAdded(); } }

        public void Execute(List<Entity> entities)
        {
            var selectedPlaceable = entities.SingleEntity().selectedPlaceable.Value;

            var tileTemplates = Pool.tileTemplates.Value.Retrieve(selectedPlaceable.Maintype);
            Pool.previewEntity.ReplaceResource(tileTemplates.Item2.First());
        }
    }
}
