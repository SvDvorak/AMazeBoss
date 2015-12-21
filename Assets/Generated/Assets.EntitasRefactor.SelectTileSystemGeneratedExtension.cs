namespace Entitas {
    public partial class Pool {
        public ISystem CreateSelectTileSystem() {
            return this.CreateSystem<Assets.EntitasRefactor.SelectTileSystem>();
        }
    }
}