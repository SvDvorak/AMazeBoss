using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;

namespace Assets
{
    public static class EntityExtensions
    {
        public static Entity AddId(this Entity entity)
        {
            var identifiables = Pools.game.GetEntities(GameMatcher.Id);
            var currentId = identifiables.Any() ? identifiables.Max(x => x.id.Value) : 0;
            entity.AddId(currentId+1);
            return entity;
        }

        public static Entity SetParent(this Entity child, Entity parent)
        {
            if(!parent.hasId)
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

        public static void AddActingAction(this Entity entity, float time, Action action)
        {
            var actingAction = new ActingAction(time, action);
            Queue<ActingAction> actions;
            if (entity.hasActingActions)
            {
                actions = entity.actingActions.Actions;
            }
            else
            {
                actions = new Queue<ActingAction>();
                actingAction.Action();
            }

            actions.Enqueue(actingAction);

            entity.ReplaceActingActions(actions);
        }

        public static void AddActingAction(this Entity entity, float time)
        {
            entity.AddActingAction(time, () => { });
        }

        public static Entity GetEntityAtPosition(this Pool pool, TilePos position, Func<Entity, bool> entityMatcher)
        {
            var entitiesAtPosition = pool.objectPositionCache.Cache[position].Where(x => entityMatcher(x)).ToList();
            if (entitiesAtPosition.Count() > 1)
            {
                throw new MoreThanOneMatchException(entitiesAtPosition);
            }

            return entitiesAtPosition.SingleOrDefault();
        }

        public class MoreThanOneMatchException : Exception
        {
            public MoreThanOneMatchException(params object[] matched) :
                base("Found multiple matches: " + string.Join(",", matched.Select(x => x.ToString()).ToArray()))
            {
            }
        }

        public static void DoForAllAtPosition(this Pool pool, TilePos position, Action<Entity> entityAction)
        {
            pool.objectPositionCache.Cache[position].ForEach(entityAction);
        }
    }
}