namespace Entitas {
    public partial class Pool {
        public ISystem CreateSpikeTrapSystem() {
            return this.CreateSystem<Assets.SpikeTrapSystem>();
        }
    }
}