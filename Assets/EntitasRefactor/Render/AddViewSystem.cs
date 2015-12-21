using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Assets.EntitasRefactor.Render
{
    public class AddViewSystem : IReactiveSystem
    {
        private readonly Transform _viewsContainer = new GameObject("Views").transform;

        public TriggerOnEvent trigger { get { return Matcher.Resource.OnEntityAddedOrRemoved(); } }

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

            var rotation = entity.hasRotation ? entity.rotation.Value : Random.Range(0, 4);
            view.transform.rotation = Quaternion.AngleAxis(rotation * 90, Vector3.up);

            entity.AddView(view);
        }

        private void RemoveView(Entity entity)
        {
            GameObject.Destroy(entity.view.Value);
            entity.RemoveView();
        }
    }
}
