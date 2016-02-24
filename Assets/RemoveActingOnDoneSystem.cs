using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

namespace Assets
{
    public class RemoveActingOnDoneSystem : IExecuteSystem, ISetPool
    {
        private Group _actingGroup;
        private Group _actingActionsGroup;

        public void SetPool(Pool pool)
        {
            _actingGroup = pool.GetGroup(GameMatcher.ActingTime);
            _actingActionsGroup = pool.GetGroup(GameMatcher.ActingActions);
        }

        public void Execute()
        {
            foreach (var acting in _actingGroup.GetEntities())
            {
                var newTimeLeft = acting.actingTime.TimeLeft - Time.deltaTime;

                if (newTimeLeft > 0)
                {
                    acting.ReplaceActingTime(newTimeLeft);
                }
                else
                {
                    acting.RemoveActingTime();
                }
            }

            var unfinishedActingEntities = _actingActionsGroup
                .GetEntities()
                .Where(x => x.actingActions.Actions.Any());

            foreach (var actingEntity in unfinishedActingEntities)
            {
                var actionQueue = actingEntity.actingActions.Actions;
                UpdateCurrentAction(actionQueue, Time.deltaTime);

                if (actionQueue.Count == 0)
                {
                    actingEntity.RemoveActingActions();
                }
            }
        }

        private static void UpdateCurrentAction(Queue<ActingAction> queue, float timePassed)
        {
            var activeAction = queue.Peek();

            var newTimeLeft = activeAction.TimeLeft - timePassed;
            if (newTimeLeft < 0)
            {
                queue.Dequeue();

                if(queue.Count > 0)
                {
                    var nextActiveAction = queue.Peek();
                    nextActiveAction.Action();
                    UpdateCurrentAction(queue, -newTimeLeft);
                }
            }
            else
            {
                activeAction.TimeLeft = newTimeLeft;
            }
        }
    }
}
