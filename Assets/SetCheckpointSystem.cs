using System.Collections.Generic;
using Assets.FileOperations;
using Entitas;

namespace Assets
{
    public class SetCheckpointSystem : IReactiveSystem, ISetPool
    {
        private Pool _pool;

        public TriggerOnEvent trigger { get { return Matcher.AllOf(GameMatcher.Hero, GameMatcher.Position).OnEntityAdded(); } }

        public void SetPool(Pool pool)
        {
            _pool = pool;
        }

        public void Execute(List<Entity> entities)
        {
            var hero = entities.SingleEntity();
            var victoryExit = _pool.GetEntityAt(hero.position.Value, x => x.isVictoryExit && !x.hasSetCheckpoint);

            if (victoryExit != null)
            {
                victoryExit.HasSetCheckpoint(true);
                PlaySetup.EditorLevel = LevelLoader.CreateLevelData(Pools.game);
                PlaySetup.FromEditor = true;
            }
        }
    }
}
