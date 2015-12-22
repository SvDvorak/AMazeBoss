using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

namespace Assets.EntitasRefactor.Preview
{
    public class PreviewTileTypeChangedSystem : PreviewChangedSystem, IReactiveSystem
    {
        public TriggerOnEvent trigger { get { return Matcher.PlaceableSelected.OnEntityAdded(); } }

        public void Execute(List<Entity> entities)
        {
            var selectedPlaceable = entities.SingleEntity().placeableSelected.Value;

            var tileTemplates = Pool.tileTemplates.Value.Retrieve(selectedPlaceable.Maintype);
            Pool.previewEntity.ReplaceResource(tileTemplates.Item2.First());
        }
    }
}
