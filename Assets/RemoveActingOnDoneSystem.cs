using Entitas;
using UnityEngine;

namespace Assets
{
    public class RemoveActingOnDoneSystem : IExecuteSystem, ISetPool
    {
        private Group _actingGroup;

        public void SetPool(Pool pool)
        {
            _actingGroup = pool.GetGroup(GameMatcher.ActingTime);
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
        }
    }
}
