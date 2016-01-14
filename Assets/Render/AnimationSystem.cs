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
            var newPosition = entity.position.Value.ToV3();
            transform.DOMove(newPosition, MoveTime).SetEase(Ease.Linear);

            // TODO! Should require animator!
            if (entity.hasAnimator && entity.IsMoving())
            {
                entity.animator.Value.SetBool("IsMoving", true);
                entity.ReplaceActingTime(MoveTime, () => entity.animator.Value.SetBool("IsMoving", false));
            }
            else
            {
                entity.ReplaceActingTime(MoveTime, () => { });
            }
            if (entity.IsMoving())
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
                entity.ReplaceActingTime(TrapActivateTime, () => entity.IsTrapActivated(false));
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
                entity.ReplaceActingTime(1, () => { });
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
}