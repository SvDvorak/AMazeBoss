using System.Collections.Generic;
using System.Linq;
using Entitas;

namespace Assets.LevelEditor
{
    public class PuzzleAreaChangedSystem : IReactiveSystem, ISetPool, IExcludeComponents
    {
        private Pool _pool;
        private BreadthFind _breadthFind;

        public TriggerOnEvent trigger { get { return GameMatcher.PuzzleArea.OnEntityAdded(); } }
        public IMatcher excludeComponents { get { return GameMatcher.BossConnection; } }

        public void SetPool(Pool pool)
        {
            _pool = pool;
            _breadthFind = new BreadthFind(_pool);
        }

        public void Execute(List<Entity> entities)
        {
            foreach (var puzzleArea in entities)
            {
                var possiblyConnectedBoss = _breadthFind.FindAllInConnection(puzzleArea, x => x.isBoss, list => list.All(x => !x.isPuzzleArea));
                if(possiblyConnectedBoss != null)
                {
                    puzzleArea.AddBossConnection(possiblyConnectedBoss.id.Value);
                }
            }
        }
    }
}