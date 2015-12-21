namespace Entitas {
    public partial class Pool {
        public ISystem CreateDestroySystem() {
            return this.CreateSystem<Assets.EntitasRefactor.DestroySystem>();
        }
    }
}