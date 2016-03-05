using System.Collections.Generic;
using Entitas;

namespace Assets.LevelEditor
{
    public class PuzzleExitConnectorSystem : IReactiveSystem, ISetPool
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
                var exitAtSamePosition = _pool.GetEntityAt(puzzleArea.position.Value, x => x.isVictoryExit);
                if (exitAtSamePosition != null)
                {
                    exitAtSamePosition.ReplaceBossConnection(puzzleArea.bossConnection.BossId);
                }
            }
        }
    }
}
