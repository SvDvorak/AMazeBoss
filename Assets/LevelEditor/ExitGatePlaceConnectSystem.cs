using System.Collections.Generic;
using System.Linq;
using Entitas;

namespace Assets.LevelEditor
{
    public class PuzzleExitGateConnectorSystem : IReactiveSystem, ISetPool
    {
        private Pool _pool;

        public TriggerOnEvent trigger { get { return Matcher.AllOf(GameMatcher.PuzzleArea, GameMatcher.BossConnection).OnEntityAdded(); } }

        public void SetPool(Pool pool)
        {
            _pool = pool;
        }

        public void Execute(List<Entity> entities)
        {
            foreach (var puzzleArea in entities)
            {
                var exitAtSamePosition = _pool.GetEntityAt(puzzleArea.position.Value, x => x.hasExitGate);
                if (exitAtSamePosition != null)
                {
                    exitAtSamePosition.ReplaceBossConnection(puzzleArea.bossConnection.BossId);
                }
            }
        }
    }

    public class ExitGateAddedSystem : IReactiveSystem, ISetPool
    {
        private Pool _pool;
        public TriggerOnEvent trigger { get { return GameMatcher.ExitGate.OnEntityAdded(); } }

        public void SetPool(Pool pool)
        {
            _pool = pool;
        }

        public void Execute(List<Entity> entities)
        {
            foreach (var exitGate in entities)
            {
                var entityWithBossConnection = _pool.GetEntitiesAt(exitGate.position.Value, x => x.hasBossConnection).FirstOrDefault();
                if (entityWithBossConnection != null)
                {
                    exitGate.ReplaceBossConnection(entityWithBossConnection.bossConnection.BossId);
                }
            }
        }
    }
}
