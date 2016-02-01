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
        public virtual IMatcher ensureComponents { get { return Matcher.Animator; } }
    }

    public class MoveAnimationSystem : IReactiveSystem, IEnsureComponents
    {
        private const float MoveTime = 0.5f;

        public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.View, Matcher.Position).OnEntityAdded(); } }
        public IMatcher ensureComponents { get { return Matcher.AnyOf(Matcher.Hero, Matcher.Boss); } }

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
            var tweener = transform.DOMove(newPosition, MoveTime).SetEase(Ease.Linear);

            // TODO! Should require animator!
            if (entity.hasAnimator && entity.IsMoving())
            {
                var animator = entity.animator.Value;
                tweener.OnStart(() => animator.SetBool("IsMoving", true));
                tweener.OnComplete(() => animator.SetBool("IsMoving", false));
            }

            entity.ReplaceActingTime(MoveTime);

            if (entity.IsMoving())
            {
                transform.rotation = Quaternion.LookRotation(newPosition - transform.position, Vector3.up);
            }
        }
    }

    public class TrapLoadedAnimationSystem : AnimationSystem, IReactiveSystem
    {
        public TriggerOnEvent trigger { get { return Matcher.Loaded.OnEntityAddedOrRemoved(); } }

        public void Execute(List<Entity> entities)
        {
            foreach (var entity in entities)
            {
                entity.animator.Value.SetBool("Loaded", entity.hasLoaded);
            }
        }
    }

    public class TrapActivatedAnimationSystem : AnimationSystem, IReactiveSystem
    {
        private const float TrapActivateTime = 0.7f;

        public TriggerOnEvent trigger { get { return Matcher.TrapActivated.OnEntityAdded(); } }
        public override IMatcher ensureComponents { get { return Matcher.AllOf(Matcher.SpikeTrap, Matcher.Loaded, base.ensureComponents); } }

        public void Execute(List<Entity> entities)
        {
            foreach (var entity in entities)
            {
                var animator = entity.animator.Value;
                DOTween.Sequence()
                    .AppendInterval(TrapActivateTime)
                    .OnStart(() => animator.SetTrigger("Activated"));
                entity.ReplaceActingTime(TrapActivateTime);
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
                entity.ReplaceActingTime(1);
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

    public class BoxKnockAnimationSystem : IInitializeSystem, ISetPool
    {
        private readonly float _startHeight = 1 - Mathf.Sin(45 * Mathf.Deg2Rad);
        private Group _boxGroup;
        private Group _cameraGroup;

        public void SetPool(Pool pool)
        {
            _cameraGroup = pool.GetGroup(Matcher.Camera);
            _boxGroup = pool.GetGroup(Matcher.AllOf(Matcher.Box, Matcher.View, Matcher.Position));
        }

        public void Initialize()
        {
            _boxGroup.OnEntityUpdated +=
                (group, entity, index, oldComponent, newComponent) =>
                    DoKnockAnimation(entity, oldComponent as PositionComponent, newComponent as PositionComponent);
        }

        private void DoKnockAnimation(Entity entity, PositionComponent oldPosition, PositionComponent newPosition)
        {
            var moveDirection = (newPosition.Value - oldPosition.Value).ToV3();
            var transform = entity.view.Value.transform;

            const float time = 0.5f;
            var cameraView = _cameraGroup.GetSingleEntity().view.Value;
            entity.AddQueueActing(time, () =>
                {
                    StartAnimation(transform, moveDirection, time);
                    cameraView.transform.DOShakeRotation(0.3f, 3, 20, 3);
                });
        }

        private void StartAnimation(Transform transform, Vector3 moveDirection, float time)
        {
            var rotationDirection = Vector3.Cross(moveDirection.normalized, Vector3.up);
            DOTween.Sequence()
                .Append(transform.DORotate(-rotationDirection * 90, time, RotateMode.WorldAxisAdd))
                .Join(transform.DOMove(moveDirection, time)
                    .SetRelative(true))
                .OnUpdate(() => UpdateVerticalMove(transform));
        }

        private void UpdateVerticalMove(Transform transform)
        {
            var angles = transform.rotation.eulerAngles;
            var position = transform.position;
            transform.position = new Vector3(position.x, Mathf.Sin((angles.x % 90 + 45) * Mathf.Deg2Rad) + _startHeight, position.z);
        }
    }
}