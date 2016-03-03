using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Entitas;

namespace Assets
{
    public static class EntityExtensions
    {
        public static Entity AddId(this Entity entity)
        {
            var identifiables = Pools.game.GetEntities(GameMatcher.Id);
            var currentId = identifiables.Any() ? identifiables.Max(x => x.id.Value) : 0;
            entity.AddId(currentId + 1);
            return entity;
        }

        public static Entity SetParent(this Entity child, Entity parent)
        {
            if (!parent.hasId)
            {
                parent.AddId();
            }
            child.AddChild(parent.id.Value);
            return child;
        }

        public static bool IsMoving(this Entity entity)
        {
            var target = entity.position.Value.ToV3();
            var current = entity.view.Value.transform.position;
            return entity.hasPosition && entity.hasView && target != current;
        }

        public static void AddActingAction(this Entity entity, float time, Sequence action)
        {
            action.Pause();
            var actingAction = new ActingAction(time, action);
            Queue<ActingAction> actions;
            if (entity.hasActingActions)
            {
                actions = entity.actingActions.Actions;
            }
            else
            {
                actions = new Queue<ActingAction>();
                actingAction.Action.Play();
            }

            actions.Enqueue(actingAction);

            entity.ReplaceActingActions(actions);
        }

        public static void AddActingAction(this Entity entity, float time, Action action)
        {
            entity.AddActingAction(time, DOTween.Sequence().OnStart(() => action()));
        }

        public static void AddActingAction(this Entity entity, float time)
        {
            entity.AddActingAction(time, DOTween.Sequence());
        }

        public static bool IsTile(this Entity entity)
        {
            return entity.IsObjectType(ObjectType.Tile);
        }

        public static bool IsItem(this Entity entity)
        {
            return entity.IsObjectType(ObjectType.Item);
        }

        public static bool IsArea(this Entity entity)
        {
            return entity.IsObjectType(ObjectType.Area);
        }

        public static bool IsObjectType(this Entity entity, ObjectType type)
        {
            return entity.gameObject.Type == type;
        }
    }
}