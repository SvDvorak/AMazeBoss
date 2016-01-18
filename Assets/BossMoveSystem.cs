using System.Collections.Generic;
using System.Linq;
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

    public class BossMoveSystem : IReactiveSystem, ISetPool
    {
        private MovementCalculator _movementCalculator;
        private Group _bossGroup;
        private Group _heroGroup;
        private Pool _pool;

        public TriggerOnEvent trigger { get { return Matcher.ActiveTurn.OnEntityAdded(); } }

        public void SetPool(Pool pool)
        {
            _pool = pool;
            _movementCalculator = new MovementCalculator(new WalkableValidator(pool));
            _bossGroup = pool.GetGroup(Matcher.Boss);
            _heroGroup = pool.GetGroup(Matcher.Hero);
        }

        public void Execute(List<Entity> entities)
        {
            foreach (var boss in _bossGroup.GetEntities().Where(x => !x.isCursed))
            {
                MoveBoss(boss);
            }
        }

        public void MoveBoss(Entity boss)
        {
            var hero = GetHero();
            var heroPosition = hero.position.Value;

            var currentMovePlan = _movementCalculator.CalculateMoveToTarget(boss.position.Value, boss.rotation.Value, heroPosition);

            if (Debug.isDebugBuild)
            {
                DrawBossPath(currentMovePlan);
            }

            if (currentMovePlan.NextStep() != heroPosition)
            {
                boss.ReplacePosition(currentMovePlan.NextStep());
            }
            else
            {
                hero.ReplaceHealth(hero.health.Value - 1);
                _pool.SwitchCurse();
            }
        }

        private void DrawBossPath(MovementCalculation currentMovePlan)
        {
            foreach (var step in currentMovePlan.Path)
            {
                Debug.DrawLine(step.ToV3(), step.ToV3() + Vector3.up*5, Color.blue, 2);
            }
        }

        private Entity GetHero()
        {
            return _heroGroup.GetSingleEntity();
        }
    }
}
