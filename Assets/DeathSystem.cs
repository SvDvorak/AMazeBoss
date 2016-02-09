using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;

namespace Assets
{
    public class DeathSystem : IReactiveSystem
    {
        public TriggerOnEvent trigger { get { return Matcher.Health.OnEntityAdded(); } }

        public void Execute(List<Entity> entities)
        {
            foreach (var deadEntity in entities.Where(x => x.health.Value <= 0))
            {
                deadEntity.isDead = true;
            }
        }
    }
}
