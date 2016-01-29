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

            var pathToHero = _movementCalculator.CalculateMoveToTarget(
                boss.position.Value,
                DirectionRotationConverter.ToDirection(boss.rotation.Value),
                heroPosition);

            if (!pathToHero.HasStepsLeft)
            {
                return;
            }

            var nextStep = pathToHero.NextStep();
            if (nextStep.Position != heroPosition)
            {
                if (Debug.isDebugBuild)
                {
                    DrawBossPath(pathToHero);
                }

                boss.ReplacePosition(nextStep.Position);

                _pool.KnockObjectsInFront(nextStep.Position, nextStep.Direction);
            }
            else
            {
                hero.ReplaceHealth(hero.health.Value - 1);
                _pool.SwitchCurse();
            }
        }

        private void DrawBossPath(Path path)
        {
            foreach (var step in path.Steps)
            {
                Debug.DrawLine(step.Position.ToV3(), step.Position.ToV3() + Vector3.up*5, Color.blue);
            }
        }

        private Entity GetHero()
        {
            return _heroGroup.GetSingleEntity();
        }
    }
}
