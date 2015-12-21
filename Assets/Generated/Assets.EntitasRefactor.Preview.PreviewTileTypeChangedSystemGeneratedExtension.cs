namespace Entitas {
    public partial class Pool {
        public ISystem CreatePreviewTileTypeChangedSystem() {
            return this.CreateSystem<Assets.EntitasRefactor.Preview.PreviewTileTypeChangedSystem>();
        }
    }
}