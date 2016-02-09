namespace Entitas {
    public partial class Pool {
        public ISystem CreateDeathSystem() {
            return this.CreateSystem<Assets.DeathSystem>();
        }
    }
}