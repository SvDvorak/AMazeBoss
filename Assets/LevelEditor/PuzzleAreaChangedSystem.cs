using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;

namespace Assets.LevelEditor
{
    public class PuzzleAreaExpandedSystem : IReactiveSystem, ISetPool, IExcludeComponents
    {
        private Pool _pool;
        private PuzzleAreaBossConnector _bossConnector;

        public TriggerOnEvent trigger { get { return GameMatcher.PuzzleArea.OnEntityAdded(); } }
        public IMatcher excludeComponents { get { return GameMatcher.BossConnection; } }

        public void SetPool(Pool pool)
        {
            _pool = pool;
            _bossConnector = new PuzzleAreaBossConnector(_pool);
        }

        public void Execute(List<Entity> entities)
        {
            foreach (var puzzleArea in entities)
            {
                _bossConnector.ConnectWholeAreaToClosestBoss(new List<TilePos>() { puzzleArea.position.Value });
            }
        }
    }

    public class PuzzleAreaShrunkSystem : IReactiveSystem, ISetPool
    {
        private Pool _pool;
        private PuzzleAreaBossConnector _bossConnector;

        public TriggerOnEvent trigger { get { return Matcher.AllOf(GameMatcher.PuzzleArea, GameMatcher.Destroyed).OnEntityAdded(); } }

        public void SetPool(Pool pool)
        {
            _pool = pool;
            _bossConnector = new PuzzleAreaBossConnector(_pool);
        }

        public void Execute(List<Entity> entities)
        {
            foreach (var puzzleArea in entities)
            {
                var area = puzzleArea;
                var neighborsToCheck = LocalDirections.GetAll()
                    .Select(localPos => localPos + area.position.Value)
                    .Where(pos => _pool.GetEntityAt(pos, e => e.isPuzzleArea) != null);

                foreach (var neighbor in neighborsToCheck)
                {
                    _bossConnector.ConnectWholeAreaToClosestBoss(new List<TilePos>() { neighbor });
                }
            }
        }
    }
}