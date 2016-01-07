namespace Entitas {
    public partial class Pool {
        public ISystem CreateSpikeSystem() {
            return this.CreateSystem<Assets.SpikeSystem>();
        }
    }
}