using System.Collections.Generic;
using DG.Tweening;
using Entitas;
using UnityEngine;

namespace Assets
{
    public class CameraFollowSystem : IReactiveSystem, ISetPool
    {
        private Group _cameraGroup;

        public TriggerOnEvent trigger { get { return Matcher.AllOf(GameMatcher.Hero, GameMatcher.Position).OnEntityAdded(); } }

        public void SetPool(Pool pool)
        {
            _cameraGroup = pool.GetGroup(GameMatcher.Camera);
        }

        public void Execute(List<Entity> entities)
        {
            var hero = entities.SingleEntity();
            var camera = _cameraGroup.GetSingleEntity();
            var newFocus = hero.position.Value.ToV3();

            camera.ReplaceTargetFocusPoint(newFocus);
        }
    }
}
