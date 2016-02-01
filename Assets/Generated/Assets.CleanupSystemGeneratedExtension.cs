namespace Entitas {
    public partial class Pool {
        public ISystem CreateCleanupSystem() {
            return this.CreateSystem<Assets.CleanupSystem>();
        }
    }
}