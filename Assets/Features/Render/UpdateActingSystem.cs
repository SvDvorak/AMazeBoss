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
        private Group _actingSequencesGroup;

        public void SetPool(Pool pool)
        {
            _actingSequencesGroup = pool.GetGroup(GameMatcher.ActingSequences);
        }

        public void Execute()
        {
            var unfinishedActingEntities = _actingSequencesGroup
                .GetEntities()
                .Where(x => x.actingSequences.Sequences.Any());

            foreach (var actingEntity in unfinishedActingEntities)
            {
                var actionQueue = actingEntity.actingSequences.Sequences;
                UpdateCurrentAction(actionQueue, Time.deltaTime);

                if (actionQueue.Count == 0)
                {
                    actingEntity.RemoveActingSequences();
                }
            }
        }

        private static void UpdateCurrentAction(Queue<ActingSequence> queue, float timePassed)
        {
            var activeAction = queue.Peek();

            var newTimeLeft = activeAction.TimeLeft - timePassed;
            if (newTimeLeft < 0)
            {
                queue.Dequeue();

                if(queue.Count > 0)
                {
                    var nextActiveAction = queue.Peek();
                    nextActiveAction.Sequence.Play();
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
