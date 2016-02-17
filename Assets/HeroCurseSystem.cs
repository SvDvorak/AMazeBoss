using System.Collections.Generic;
using Entitas;

namespace Assets
{
    public class HeroCurseSystem : IReactiveSystem, ISetPool
    {
        private Pool _pool;

        public TriggerOnEvent trigger { get { return Matcher.AllOf(GameMatcher.Hero, GameMatcher.InputCurseSwitch).OnEntityAdded(); } }

        public void SetPool(Pool pool)
        {
            _pool = pool;
        }

        public void Execute(List<Entity> entities)
        {
            _pool.SwitchCurse();
        }
    }
}