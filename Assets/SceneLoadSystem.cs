using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entitas;

namespace Assets
{
    public class SceneLoadSystem : IReactiveSystem
    {
        public TriggerOnEvent trigger { get; set; }

        public void Execute(List<Entity> entities)
        {
        }
    }
}
