using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Assets.Render
{
    public class PreviewMaterialChangeSystem : IReactiveSystem
    {
        public TriggerOnEvent trigger { get { return Matcher.AllOf(GameMatcher.Preview, GameMatcher.View).OnEntityAdded(); } }

        public void Execute(List<Entity> entities)
        {
            var preview = entities.SingleEntity();
            var previewMaterial = Resources.Load<Material>("Preview");
            var renderer = preview.view.Value.GetComponentInChildren<Renderer>();
            renderer.material = previewMaterial;
        }
    }
}
