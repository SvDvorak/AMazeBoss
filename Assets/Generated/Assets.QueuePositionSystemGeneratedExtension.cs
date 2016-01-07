namespace Entitas {
    public partial class Pool {
        public ISystem CreateQueuePositionSystem() {
            return this.CreateSystem<Assets.QueuePositionSystem>();
        }
    }
}