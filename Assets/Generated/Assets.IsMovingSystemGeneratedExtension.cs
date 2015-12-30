namespace Entitas {
    public partial class Pool {
        public ISystem CreateIsMovingSystem() {
            return this.CreateSystem<Assets.IsMovingSystem>();
        }
    }
}