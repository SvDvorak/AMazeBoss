namespace Entitas {
    public partial class Pool {
        public ISystem CreateSelectTileSystem() {
            return this.CreateSystem<Assets.EntitasRefactor.Input.SelectTileSystem>();
        }
    }
}