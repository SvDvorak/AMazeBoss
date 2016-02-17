using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Assets.Render
{
    public class SetInitialTransformSystem : IReactiveSystem
    {
        public TriggerOnEvent trigger { get { return GameMatcher.View.OnEntityAdded(); } }

        public void Execute(List<Entity> entities)
        {
            foreach (var entity in entities)
            {
                var view = entity.view.Value;

                entity.ReplaceViewOffset(view.transform.position);

                if (entity.hasPosition)
                {
                    view.transform.position = entity.position.Value.ToV3() + entity.viewOffset.Value;
                }

                if (entity.hasRotation)
                {
                    view.transform.rotation = Quaternion.AngleAxis(entity.rotation.Value * 90, Vector3.up);
                }
            }
        }
    }
}