namespace Entitas {
    public partial class Pool {
        public ISystem CreateTickSystem() {
            return this.CreateSystem<Assets.TickSystem>();
        }
    }
}