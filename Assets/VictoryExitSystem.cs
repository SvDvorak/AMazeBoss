using System;
using System.Collections.Generic;
using Entitas;

namespace Assets
{
    public class VictoryExitSystem : IReactiveSystem, ISetPool
    {
        private Group _bossExitsGroup;
        private Pool _pool;

        public TriggerOnEvent trigger { get { return Matcher.AllOf(GameMatcher.Boss, GameMatcher.Dead).OnEntityAdded(); } }

        public void SetPool(Pool pool)
        {
            _pool = pool;
            _bossExitsGroup = pool.GetGroup(GameMatcher.VictoryExit);
        }

        public void Execute(List<Entity> entities)
        {
            _bossExitsGroup.GetEntities().DoForAll(x => x.IsBlockingTile(false));
            _pool.SwitchCurse();
        }
    }
}
