namespace Entitas {
    public partial class Pool {
        public ISystem CreatePlaceTileSystem() {
            return this.CreateSystem<Assets.EntitasRefactor.Input.PlaceTileSystem>();
        }
    }
}