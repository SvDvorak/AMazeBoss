using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;

namespace Assets
{
    public class CurseSwitchSystem : IReactiveSystem, ISetPool
    {
        private Pool _pool;
        private Group _curseSwitchGroup;

        public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.Boss, Matcher.Position).OnEntityAdded(); } }

        public void SetPool(Pool pool)
        {
            _pool = pool;
            _curseSwitchGroup = pool.GetGroup(Matcher.AllOf(Matcher.CurseSwitch, Matcher.Position));
        }

        public void Execute(List<Entity> entities)
        {
            var boss = entities.SingleEntity();

            foreach (var curseSwitch in _curseSwitchGroup.GetEntities())
            {
                var bossAtSamePosition = boss.position.Value == curseSwitch.position.Value;
                if (bossAtSamePosition && !boss.isCursed)
                {
                    _pool.SwitchCurse();
                }

                curseSwitch.IsTrapActivated(bossAtSamePosition);
            }
        }
    }
}
