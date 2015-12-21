using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Assets.EntitasRefactor.Render
{
    public class PreviewMaterialChangeSystem : IReactiveSystem
    {
        public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.Preview, Matcher.View).OnEntityAdded(); } }

        public void Execute(List<Entity> entities)
        {
            var preview = entities.SingleEntity();
            var previewMaterial = Resources.Load<Material>("Preview");
            preview.view.Value.GetComponent<MeshRenderer>().material = previewMaterial;
        }
    }
}
