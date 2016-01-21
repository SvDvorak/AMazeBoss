using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;
using DG.Tweening;
using UnityEngine;

namespace Assets.Render
{
    public abstract class AnimationSystem : IEnsureComponents
    {
        public IMatcher ensureComponents { get { return Matcher.Animator; } }
    }

    public class PositionAnimationSystem : IReactiveSystem, IEnsureComponents
    {
        private const float MoveTime = 0.5f;

        public TriggerOnEvent trigger { get { return Matcher.Position.OnEntityAddedOrRemoved(); } }
        public IMatcher ensureComponents { get { return Matcher.View; } }

        public void Execute(List<Entity> entities)
        {
            foreach (var entity in entities)
            {
                PositionChanged(entity);
            }
        }

        private void PositionChanged(Entity entity)
        {
            var transform = entity.view.Value.transform;
            var newPosition = entity.position.Value.ToV3() + entity.viewOffset.Value;
            transform.DOMove(newPosition, MoveTime).SetEase(Ease.Linear);

            // TODO! Should require animator!
            if (entity.hasAnimator && entity.IsMoving())
            {
                entity.animator.Value.SetBool("IsMoving", true);
                entity.UpdateActingTime(MoveTime, () => entity.animator.Value.SetBool("IsMoving", false));
            }
            else
            {
                entity.UpdateActingTime(MoveTime, () => { });
            }
            if (entity.IsMoving() && (entity.isBoss || entity.isHero))
            {
                transform.rotation = Quaternion.LookRotation(newPosition - transform.position, Vector3.up);
            }
        }
    }

    public class TrapLoadedAnimationSystem : AnimationSystem, IReactiveSystem
    {
        public TriggerOnEvent trigger { get { return Matcher.SpikeTrap.OnEntityAddedOrRemoved(); } }

        public void Execute(List<Entity> entities)
        {
            foreach (var entity in entities)
            {
                entity.animator.Value.SetBool("Loaded", entity.spikeTrap.IsLoaded);
            }
        }
    }

    public class TrapActivatedAnimationSystem : AnimationSystem, IReactiveSystem
    {
        private const int TrapActivateTime = 1;

        public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.SpikeTrap, Matcher.TrapActivated).OnEntityAdded(); } }

        public void Execute(List<Entity> entities)
        {
            foreach (var entity in entities)
            {
                entity.animator.Value.SetTrigger("Activated");
                entity.UpdateActingTime(TrapActivateTime, () => entity.IsTrapActivated(false));
            }
        }
    }

    public class CurseSwitchActivatedAnimationSystem : AnimationSystem, IReactiveSystem
    {
        public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.CurseSwitch, Matcher.TrapActivated).OnEntityAddedOrRemoved(); } }

        public void Execute(List<Entity> entities)
        {
            foreach (var entity in entities)
            {
                entity.animator.Value.SetBool("WeightedDown", entity.isTrapActivated);
                entity.UpdateActingTime(1, () => { });
            }
        }
    }

    public class HealthChangedAnimationSystem : IReactiveSystem
    {
        public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.HealthVisual, Matcher.Health).OnEntityAdded(); } }

        public void Execute(List<Entity> entities)
        {
            foreach (var entity in entities)
            {
                entity.healthVisual.Text.text = entity.health.Value.ToString();
            }
        }
    }

    public class BoxRotateAnimationSystem : IInitializeSystem, ISetPool
    {
        private readonly float _startHeight = 1 - Mathf.Sin(45*Mathf.Deg2Rad);
        private Group _boxGroup;

        public void SetPool(Pool pool)
        {
            _boxGroup = pool.GetGroup(Matcher.AllOf(Matcher.Box, Matcher.View, Matcher.Position));
        }

        public void Initialize()
        {
            _boxGroup.OnEntityUpdated +=
                (group, entity, index, oldComponent, newComponent) =>
                    DoRotationAnimation(entity, oldComponent as PositionComponent, newComponent as PositionComponent);
        }

        private void DoRotationAnimation(Entity entity, PositionComponent oldPosition, PositionComponent newPosition)
        {
            var moveDirection = (newPosition.Value - oldPosition.Value).ToV3();
            var rotationDirection = Vector3.Cross(moveDirection.normalized, Vector3.up);
            var transform = entity.view.Value.transform;

            const float time = 0.5f;
            DOTween.Sequence()
                .Append(transform.DORotate(-rotationDirection*90, time, RotateMode.WorldAxisAdd)
                    .OnUpdate(() => UpdateVerticalMove(transform)))
                .SetEase(Ease.InCirc);
            entity.UpdateActingTime(time, () => { });
        }

        private void UpdateVerticalMove(Transform transform)
        {
            var angles = transform.rotation.eulerAngles;
            var position = transform.position;
            transform.position = new Vector3(position.x, Mathf.Sin((angles.x % 90 + 45) * Mathf.Deg2Rad) + _startHeight, position.z);
        }
    }
}