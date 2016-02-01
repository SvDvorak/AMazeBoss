using System.Collections.Generic;
using Entitas;

namespace Assets.Input
{
    public class PerformInputQueueSystem : IReactiveSystem
    {
        public TriggerOnEvent trigger { get {return Matcher.AllOf(Matcher.InputQueue, Matcher.ActiveTurn).OnEntityAdded(); } }

        public void Execute(List<Entity> entities)
        {
            foreach (var entity in entities)
            {
                entity.inputQueue.InputAction();
                entity.RemoveInputQueue();
            }
        }
    }
}