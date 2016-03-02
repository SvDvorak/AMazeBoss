using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

namespace Assets.Render
{
    public class AddOrRemoveViewSystem : IReactiveSystem, ISetPool
    {
        private readonly Transform _viewsContainer = new GameObject("Views").transform;
        private Pool _pool;

        public TriggerOnEvent trigger { get { return GameMatcher.Resource.OnEntityAddedOrRemoved(); } }

        public void SetPool(Pool pool)
        {
            _pool = pool;
        }

        public void Execute(List<Entity> entities)
        {
            foreach (var entity in entities.Where(x => x.hasView))
            {
                DestroySystem.DestroyView(entity);
            }

            foreach (var entity in entities.Where(x => x.hasResource && (_pool.isInEditor || !x.hasEditorOnlyVisual)))
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

            entity.AddView(view);
            GameObjectConfigurer.AttachEntity(view, entity);
        }
    }
}
