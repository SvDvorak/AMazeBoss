namespace Entitas {
    public partial class Pool {
        public ISystem CreatePreviewTilePositionChangedSystem() {
            return this.CreateSystem<Assets.EntitasRefactor.Preview.PreviewTilePositionChangedSystem>();
        }
    }
}