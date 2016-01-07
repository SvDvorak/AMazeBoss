using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Assets
{
    public interface IGameObjectConfigurer
    {
        void OnAttachEntity(Entity entity);
        void OnDetachEntity(Entity entity);
    }

    public static class GameObjectConfigurer
    {
        private static readonly List<IGameObjectConfigurer> CachedEntityList;
        static GameObjectConfigurer()
        {
            CachedEntityList = new List<IGameObjectConfigurer>(16);
        }

        public static void AttachEntity(GameObject unityObject, Entity entity)
        {
            PerformForEachConfigurer(unityObject, conf => conf.OnAttachEntity(entity));
        }

        public static void DetachEntity(GameObject unityObject, Entity entity)
        {
            PerformForEachConfigurer(unityObject, conf => conf.OnDetachEntity(entity));
        }

        private static void PerformForEachConfigurer(GameObject unityObject, Action<IGameObjectConfigurer> action)
        {
            CachedEntityList.Clear();
            unityObject.GetComponents(CachedEntityList);
            foreach (var t in CachedEntityList)
            {
                action(t);
            }
        }
    }
}
