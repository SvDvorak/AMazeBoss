using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Assets.Render
{
    public class AddViewSystem : IReactiveSystem
    {
        private readonly Transform _viewsContainer = new GameObject("Views").transform;

        public TriggerOnEvent trigger { get { return GameMatcher.Resource.OnEntityAddedOrRemoved(); } }

        public void Execute(List<Entity> entities)
        {
            foreach (var entity in entities)
            {
                Execute(entity);
            }
        }

        private void Execute(Entity entity)
        {
            if (entity.hasView)
            {
                RemoveView(entity);
            }

            if (entity.hasResource)
            {
                AddView(entity);
            }
        }

        private void AddView(Entity entity)
        {
            var resourceObject = Resources.Load<GameObject>(entity.resource.Path);
            if (resourceObject == null)
            {
                throw new MissingReferenceException("Resource " + entity.resource.Path + " not found.");
            }

            var view = GameObject.Instantiate(resourceObject);
            view.transform.SetParent(_viewsContainer);

            entity.ReplaceViewOffset(view.transform.position);

            if (entity.hasPosition)
            {
                view.transform.position = entity.position.Value.ToV3() + entity.viewOffset.Value;
            }

            if (entity.hasRotation)
            {
                view.transform.rotation = Quaternion.AngleAxis(entity.rotation.Value * 90, Vector3.up);
            }

            entity.AddView(view);
            GameObjectConfigurer.AttachEntity(view, entity);
        }

        private void RemoveView(Entity entity)
        {
            GameObjectConfigurer.DetachEntity(entity.view.Value, entity);
            GameObject.Destroy(entity.view.Value);
            entity.RemoveView();
        }
    }
}
