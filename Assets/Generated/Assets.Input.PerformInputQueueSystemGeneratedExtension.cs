namespace Entitas {
    public partial class Pool {
        public ISystem CreatePerformInputQueueSystem() {
            return this.CreateSystem<Assets.Input.PerformInputQueueSystem>();
        }
    }
}