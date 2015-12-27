using Entitas;
using UnityEngine;

namespace Assets
{
    public class WalkableValidator : IWalkableValidator
    {
        private readonly Pool _pool;

        public WalkableValidator(Pool pool)
        {
            _pool = pool;
        }

        public bool CanMoveTo(TilePos position)
        {
            return _pool.CanMoveTo(position);
        }
    }

    public class BossMoveSystem : IExecuteSystem, ISetPool
    {
        private MovementCalculator _movementCalculator;
        private Group _bossGroup;
        private Group _heroGroup;

        public void SetPool(Pool pool)
        {
            _movementCalculator = new MovementCalculator(new WalkableValidator(pool));
            _bossGroup = pool.GetGroup(Matcher.Boss);
            _heroGroup = pool.GetGroup(Matcher.Hero);
        }

        public void Execute()
        {
            foreach (var boss in _bossGroup.GetEntities())
            {
                MoveBoss(boss);
            }
        }

        public void MoveBoss(Entity boss)
        {
            if (!boss.hasThinkDelay)
            {
                var targetPosition = GetHeroPosition();

                var currentMovePlan = _movementCalculator.CalculateMoveToTarget(boss.position.Value, targetPosition);

                if (currentMovePlan.HasStepsLeft)
                {
                    boss.ReplacePosition(currentMovePlan.NextStep());
                }

                boss.AddThinkDelay(0.5f);
            }
            else
            {
                boss.ReplaceThinkDelay(boss.thinkDelay.TimeLeft - Time.deltaTime);

                if (boss.thinkDelay.TimeLeft < 0)
                {
                    boss.RemoveThinkDelay();
                }
            }
        }

        private TilePos GetHeroPosition()
        {
            return _heroGroup.GetSingleEntity().position.Value;
        }
    }
}
