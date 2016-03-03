using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Entitas;
using UnityEngine;

namespace Assets
{
    public class UpdateActingSystem : IExecuteSystem, ISetPool
    {
        private Group _actingActionsGroup;

        public void SetPool(Pool pool)
        {
            _actingActionsGroup = pool.GetGroup(GameMatcher.ActingActions);
        }

        public void Execute()
        {
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
                    nextActiveAction.Action.Play();
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
