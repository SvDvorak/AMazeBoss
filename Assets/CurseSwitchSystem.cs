using System.Collections.Generic;
using System.Linq;
using Entitas;

namespace Assets
{
    public class CurseSwitchSystem : IReactiveSystem, ISetPool
    {
        private Pool _pool;

        public TriggerOnEvent trigger { get { return Matcher.AllOf(GameMatcher.Boss, GameMatcher.Position).OnEntityAdded(); } }

        public void SetPool(Pool pool)
        {
            _pool = pool;
        }

        public void Execute(List<Entity> entities)
        {
            var isSomeBossStandingOnSwitch = entities.Any(boss =>
                _pool.GetEntityAtPosition(boss.position.Value, e => e.isCurseSwitch) != null);

            if (isSomeBossStandingOnSwitch)
            {
                _pool.SwitchCurse();
            }
        }
    }
}
